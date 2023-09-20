using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using Squirrel.AzureBlobStorage.Interfaces;
using Squirrel.Shared.Enums;
using Squirrel.Shared.DTO.DatabaseItem;
using Squirrel.Shared.DTO.Text;
using Squirrel.SqlService.BLL.Interfaces;
using Squirrel.SqlService.BLL.Models.DTO;
using System.Text;
using Blob = Squirrel.AzureBlobStorage.Models.Blob;
using Squirrel.SqlService.BLL.Models.DTO.Function;
using Squirrel.SqlService.BLL.Models.DTO.Procedure;
using Squirrel.SqlService.BLL.Models.DTO.View;
using System.Reflection.Metadata;

namespace Squirrel.SqlService.BLL.Services;

public class ContentDifferenceService : IContentDifferenceService
{
    private readonly IBlobStorageService _blobStorageService;
    private readonly IConfiguration _configuration;
    private readonly ITextService _textService;

    public ContentDifferenceService(IBlobStorageService blobStorageService, IConfiguration configuration, ITextService textService)
    {
        _blobStorageService = blobStorageService;
        _textService = textService;
        _configuration = configuration;
    }

    public async Task<List<DatabaseItemContentCompare>> GetContentDiffsAsync(int commitId, Guid tempBlobId)
    {
        return await GetTextPairFromBlobs(commitId, tempBlobId);
    }

    private async Task<List<DatabaseItemContentCompare>> GetTextPairFromBlobs(int commitId, Guid tempBlobId)
    {
        var containers = await _blobStorageService.GetContainersByPrefixAsync(commitId.ToString());
        var dbStructure = await GetTempBlobContentAsync(tempBlobId);
        var differenceList = new List<DatabaseItemContentCompare>();
        var markedBlobIds = new List<string>();

        await CompareDbItemsContent(dbStructure.DbTableStructures!, containers, commitId, DatabaseItemType.Table, differenceList, markedBlobIds);

        foreach (var tableConstraints in dbStructure.DbConstraints!)
        {
            await CompareDbItemsContent(tableConstraints.Constraints, containers, commitId, DatabaseItemType.Constraint, differenceList, markedBlobIds);
        }
        
        await CompareDbItemsContent(dbStructure.DbFunctionDetails!.Details, containers, commitId, DatabaseItemType.Function, differenceList, markedBlobIds);
        await CompareDbItemsContent(dbStructure.DbProcedureDetails!.Details, containers, commitId, DatabaseItemType.StoredProcedure, differenceList, markedBlobIds);
        await CompareDbItemsContent(dbStructure.DbViewsDetails!.Details, containers, commitId, DatabaseItemType.View, differenceList, markedBlobIds);

        var tableContainer = GetContainerName(commitId, DatabaseItemType.Table);
        await CompareUnmarkedBlobsContent<TableStructureDto>(DatabaseItemType.Table, tableContainer, differenceList, markedBlobIds);
        
        var constraintContainer = GetContainerName(commitId, DatabaseItemType.Constraint);
        await CompareUnmarkedConstraintBlobsContent(DatabaseItemType.Constraint, constraintContainer, differenceList, markedBlobIds);
        
        var functionContainer = GetContainerName(commitId, DatabaseItemType.Function);
        await CompareUnmarkedBlobsContent<FunctionDetailInfo>(DatabaseItemType.Function, functionContainer, differenceList, markedBlobIds);
        
        var spContainer = GetContainerName(commitId, DatabaseItemType.StoredProcedure);
        await CompareUnmarkedBlobsContent<ProcedureDetailInfo>(DatabaseItemType.StoredProcedure, spContainer, differenceList, markedBlobIds);

        var viewContainer = GetContainerName(commitId, DatabaseItemType.View);
        await CompareUnmarkedBlobsContent<ViewDetailInfo>(DatabaseItemType.View, viewContainer, differenceList, markedBlobIds);
        
        return differenceList;
    }

    private async Task CompareUnmarkedBlobsContent<T>(DatabaseItemType itemType, string containerName, List<DatabaseItemContentCompare> differenceList,
        List<string> markedBlobIds) where T : BaseDbItem
    {
        var blobs = await _blobStorageService.GetAllBlobsByContainerNameAsync(containerName);
        var unmarkedBlobs = blobs.Where(blob => !markedBlobIds.Contains(blob.Id));
        foreach (var blob in unmarkedBlobs)
        {
            differenceList.Add(GetDbItemDifference<T>(blob.Content!, itemType));
        }
    }

    private async Task CompareUnmarkedConstraintBlobsContent(DatabaseItemType itemType, string containerName, List<DatabaseItemContentCompare> differenceList,
    List<string> markedBlobIds)
    {
        var blobs = await _blobStorageService.GetAllBlobsByContainerNameAsync(containerName);
        var unmarkedBlobs = blobs.Where(blob => !markedBlobIds.Contains(blob.Id));
        foreach (var blob in unmarkedBlobs)
        {
            CheckBlockContentNotNull(blob.Content);
            var jsonString = Encoding.UTF8.GetString(blob.Content);
            var tableConstraintsDto = JsonConvert.DeserializeObject<TableConstraintsDto>(jsonString)!;
            foreach (var constraint in tableConstraintsDto.Constraints)
            {
                var textPair = new TextPairRequestDto
                {
                    OldText = JsonConvert.SerializeObject(constraint),
                    NewText = string.Empty,
                    IgnoreWhitespace = true
                };
                var dbItemContentCompare = new DatabaseItemContentCompare
                {
                    SchemaName = constraint.Schema,
                    ItemName = constraint.Name,
                    ItemType = itemType,
                    InLineDiff = _textService.GetInlineDiffs(textPair),
                    SideBySideDiff = _textService.GetSideBySideDiffs(textPair),
                };
                differenceList.Add(dbItemContentCompare);
            }
        }
    }

    private async Task CompareDbItemsContent<T>(List<T> dbStructureItemCollection, ICollection<string> containers,
        int commitId, DatabaseItemType itemType, List<DatabaseItemContentCompare> differenceList, List<string> markedBlobIds) where T : BaseDbItem
    {
        ICollection<Blob> blobs = new List<Blob>();
        var dbItemContainer = containers.FirstOrDefault(cont => cont == GetContainerName(commitId, itemType));
        if (dbItemContainer is not null)
        {
            blobs = await _blobStorageService.GetAllBlobsByContainerNameAsync(dbItemContainer);
        }
        foreach (T dbItem in dbStructureItemCollection)
        {
            var currentBlob = blobs.FirstOrDefault(blob => blob.Id == GetBlobName(dbItem.Schema, dbItem.Name));
            if (currentBlob is null)
            {
                differenceList.Add(GetDbItemDifference(Encoding.UTF8.GetBytes(""), dbItem, itemType));
                continue;
            }
            differenceList.Add(GetDbItemDifference(currentBlob.Content!, dbItem, itemType));
            markedBlobIds.Add(currentBlob.Id);
        }
    }

    private DatabaseItemContentCompare GetDbItemDifference<T>(byte[] blobContent, T dbItem,
        DatabaseItemType itemType) where T : BaseDbItem
    {
        CheckBlockContentNotNull(blobContent);

        var commitItemContent = DeserializeBlobContent<T>(blobContent);

        var textPair = new TextPairRequestDto
        {
            OldText = JsonConvert.SerializeObject(commitItemContent),
            NewText = JsonConvert.SerializeObject(dbItem),
            IgnoreWhitespace = true
        };
        var dbItemContentCompare = new DatabaseItemContentCompare
        {
            SchemaName = dbItem.Schema,
            ItemName = dbItem.Name,
            ItemType = itemType,
            InLineDiff = _textService.GetInlineDiffs(textPair),
            SideBySideDiff = _textService.GetSideBySideDiffs(textPair),
        };
        return dbItemContentCompare;
    }

    private DatabaseItemContentCompare GetDbItemDifference<T>(byte[] blobContent, DatabaseItemType itemType) where T: BaseDbItem
    {
        CheckBlockContentNotNull(blobContent);
        
        var commitItemContent = DeserializeBlobContent<T>(blobContent);

        var textPair = new TextPairRequestDto
        {
            OldText = JsonConvert.SerializeObject(commitItemContent),
            NewText = string.Empty,
            IgnoreWhitespace = true
        };
        var dbItemContentCompare = new DatabaseItemContentCompare
        {
            SchemaName = commitItemContent.Schema,
            ItemName = commitItemContent.Name,
            ItemType = itemType,
            InLineDiff = _textService.GetInlineDiffs(textPair),
            SideBySideDiff = _textService.GetSideBySideDiffs(textPair),
        };
        return dbItemContentCompare;
    }

    private static T DeserializeBlobContent<T>(byte[] blobContent) where T : BaseDbItem
    {
        var jsonString = Encoding.UTF8.GetString(blobContent);
        var commitItemContent = JsonConvert.DeserializeObject<T>(jsonString)!;
        return commitItemContent;
    }

    private void CheckBlockContentNotNull(byte[] blobContent)
    {
        if (blobContent is null)
        {
            throw new Exception("Blob Content is empty");
        }
    }

    private async Task<DbStructureDto> GetTempBlobContentAsync(Guid tempBlobId)
    {
        var blob = await _blobStorageService.DownloadAsync(_configuration["UserDbChangesBlobContainerName"], tempBlobId.ToString());
        if (blob.Content is not null)
        {
            var jsonString = Encoding.UTF8.GetString(blob.Content);
            return JsonConvert.DeserializeObject<DbStructureDto>(jsonString)!;
        }
        throw new Exception("Blob Content is empty");
    }

    private string GetContainerName(int commitId, DatabaseItemType itemType)
    {
        return $"{commitId}-{itemType}".ToLower();
    }
    private string GetBlobName(string schema, string name)
    {
        return $"{schema}-{name}".ToLower();
    }
}
