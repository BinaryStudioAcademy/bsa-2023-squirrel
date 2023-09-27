using System.Text;
using Microsoft.Extensions.Configuration;
using Microsoft.SqlServer.TransactSql.ScriptDom;
using Newtonsoft.Json;
using Squirrel.AzureBlobStorage.Interfaces;
using Squirrel.Shared.Enums;
using Squirrel.Shared.DTO.DatabaseItem;
using Squirrel.Shared.DTO.Text;
using Squirrel.SqlService.BLL.Interfaces;
using Squirrel.Shared.DTO;
using Squirrel.Shared.DTO.Table;
using Squirrel.Shared.DTO.Function;
using Squirrel.Shared.DTO.Procedure;
using Squirrel.Shared.DTO.UserDefinedType.DataType;
using Squirrel.Shared.DTO.UserDefinedType.TableType;
using Squirrel.Shared.DTO.View;
using Blob = Squirrel.AzureBlobStorage.Models.Blob;

namespace Squirrel.SqlService.BLL.Services;

public class ContentDifferenceService : IContentDifferenceService
{
    private const string UserDbChangesBlobContainerNameSection = "UserDbChangesBlobContainerName";
    private readonly IBlobStorageService _blobStorageService;
    private readonly IConfiguration _configuration;
    private readonly ITextService _textService;
    private readonly IDbItemsRetrievalService _dbItemsRetrievalService;

    public ContentDifferenceService(IBlobStorageService blobStorageService, IConfiguration configuration, ITextService textService, 
        IDbItemsRetrievalService dbItemsRetrievalService)
    {
        _blobStorageService = blobStorageService;
        _textService = textService;
        _configuration = configuration;
        _dbItemsRetrievalService = dbItemsRetrievalService;
    }

    public async Task<ICollection<DatabaseItemContentCompare>> GetContentDiffsAsync(int commitId, Guid tempBlobId)
    {
        return await GetTextPairFromBlobsAsync(commitId, tempBlobId);
    }

    private async Task<ICollection<DatabaseItemContentCompare>> GetTextPairFromBlobsAsync(int commitId, Guid tempBlobId)
    {
        var containers = await _blobStorageService.GetContainersByPrefixAsync(commitId.ToString());
        var dbStructure = await GetTempBlobContentAsync(tempBlobId);
        var differenceList = new List<DatabaseItemContentCompare>();
        var markedBlobIds = new List<string>();

        await CompareDbItemsContentAsync(dbStructure.DbTableStructures!, containers, commitId, DatabaseItemType.Table, differenceList, markedBlobIds);

        foreach (var tableConstraints in dbStructure.DbConstraints!)
        {
            await CompareDbItemsContentAsync(tableConstraints.Constraints, containers, commitId, DatabaseItemType.Constraint, differenceList, markedBlobIds);
        }
        
        await CompareDbItemsContentAsync(dbStructure.DbFunctionDetails!.Details, containers, commitId, DatabaseItemType.Function, differenceList, markedBlobIds);
        await CompareDbItemsContentAsync(dbStructure.DbProcedureDetails!.Details, containers, commitId, DatabaseItemType.StoredProcedure, differenceList, markedBlobIds);
        await CompareDbItemsContentAsync(dbStructure.DbViewsDetails!.Details, containers, commitId, DatabaseItemType.View, differenceList, markedBlobIds);
        await CompareDbItemsContentAsync(dbStructure.DbUserDefinedDataTypeDetailsDto.Details, containers, commitId,
            DatabaseItemType.UserDefinedDataType, differenceList, markedBlobIds);

        await CompareDbItemsContentAsync(dbStructure.DbUserDefinedTableTypeDetailsDto.Tables, containers, commitId, DatabaseItemType.UserDefinedTableType,
            differenceList, markedBlobIds);
        
        var tableContainer = GetContainerName(commitId, DatabaseItemType.Table);
        await CompareUnmarkedBlobsContentAsync<TableStructureDto>(DatabaseItemType.Table, tableContainer, differenceList, markedBlobIds);
        
        var constraintContainer = GetContainerName(commitId, DatabaseItemType.Constraint);
        await CompareUnmarkedConstraintBlobsContentAsync(DatabaseItemType.Constraint, constraintContainer, differenceList, markedBlobIds);
        
        var functionContainer = GetContainerName(commitId, DatabaseItemType.Function);
        await CompareUnmarkedBlobsContentAsync<FunctionDetailInfo>(DatabaseItemType.Function, functionContainer, differenceList, markedBlobIds);
        
        var spContainer = GetContainerName(commitId, DatabaseItemType.StoredProcedure);
        await CompareUnmarkedBlobsContentAsync<ProcedureDetailInfo>(DatabaseItemType.StoredProcedure, spContainer, differenceList, markedBlobIds);

        var viewContainer = GetContainerName(commitId, DatabaseItemType.View);
        await CompareUnmarkedBlobsContentAsync<ViewDetailInfo>(DatabaseItemType.View, viewContainer, differenceList, markedBlobIds);

        var udDataTypeContainer = GetContainerName(commitId, DatabaseItemType.UserDefinedDataType);
        await CompareUnmarkedBlobsContentAsync<UserDefinedDataTypeDetailInfo>(DatabaseItemType.UserDefinedDataType, 
            udDataTypeContainer, differenceList, markedBlobIds);

        var udTableTypeContainer = GetContainerName(commitId, DatabaseItemType.UserDefinedTableType);
        await CompareUnmarkedBlobsContentAsync<UserDefinedTableDetailsDto>(DatabaseItemType.UserDefinedTableType,
            udTableTypeContainer, differenceList, markedBlobIds);
        
        return differenceList;
    }

    private async Task CompareUnmarkedBlobsContentAsync<T>(DatabaseItemType itemType, string containerName, List<DatabaseItemContentCompare> differenceList,
        List<string> markedBlobIds) where T : BaseDbItem
    {
        var blobs = await _blobStorageService.GetAllBlobsByContainerNameAsync(containerName);
        var unmarkedBlobs = blobs.Where(blob => !markedBlobIds.Contains(blob.Id));
        foreach (var blob in unmarkedBlobs)
        {
            differenceList.Add(GetDbItemDifference<T>(blob.Content!, itemType));
        }
    }

    private async Task CompareUnmarkedConstraintBlobsContentAsync(DatabaseItemType itemType, string containerName, List<DatabaseItemContentCompare> differenceList,
        ICollection<string> markedBlobIds)
    {
        var blobs = await _blobStorageService.GetAllBlobsByContainerNameAsync(containerName);
        var unmarkedBlobs = blobs.Where(blob => !markedBlobIds.Contains(blob.Id));
        foreach (var blob in unmarkedBlobs)
        {
            CheckBlockContentNotNull(blob.Content!);
            var jsonString = Encoding.UTF8.GetString(blob.Content!);
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

    private async Task CompareDbItemsContentAsync<T>(List<T> dbStructureItemCollection, ICollection<string> containers,
        int commitId, DatabaseItemType itemType, ICollection<DatabaseItemContentCompare> differenceList, ICollection<string> markedBlobIds)
            where T : BaseDbItem
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

    private DatabaseItemContentCompare GetDbItemDifference<T>(byte[] blobContent, DatabaseItemType itemType) 
        where T : BaseDbItem
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
        var blob = await _blobStorageService.DownloadAsync(_configuration[UserDbChangesBlobContainerNameSection], tempBlobId.ToString());
        if (blob.Content is not null)
        {
            var jsonString = Encoding.UTF8.GetString(blob.Content);
            return JsonConvert.DeserializeObject<DbStructureDto>(jsonString)!;
        }
        throw new Exception("Blob Content is empty");
    }

    private string GetContainerName(int commitId, DatabaseItemType itemType) => $"{commitId}-{itemType}".ToLower();

    private string GetBlobName(string schema, string name) => $"{schema}-{name}".ToLower();
}