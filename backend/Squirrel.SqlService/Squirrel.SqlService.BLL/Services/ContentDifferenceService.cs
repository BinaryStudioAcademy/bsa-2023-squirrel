using System.Text;
using Microsoft.Extensions.Configuration;
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
using Squirrel.Core.DAL.Entities;

namespace Squirrel.SqlService.BLL.Services;

public class ContentDifferenceService : IContentDifferenceService
{
    private const string UserDbChangesBlobContainerNameSection = "UserDbChangesBlobContainerName";
    private readonly IBlobStorageService _blobStorageService;
    private readonly IConfiguration _configuration;
    private readonly ITextService _textService;

    public ContentDifferenceService(IBlobStorageService blobStorageService, IConfiguration configuration, ITextService textService)
    {
        _blobStorageService = blobStorageService;
        _textService = textService;
        _configuration = configuration;
    }

    public async Task<ICollection<DatabaseItemContentCompare>> GetContentDiffsAsync(int commitId, Guid tempBlobId, bool isReverse = false)
    {
        var containers = await _blobStorageService.GetContainersByPrefixAsync(commitId.ToString());
        var dbStructure = await GetTempBlobContentAsync(tempBlobId);
        return await GetTextPairFromBlobsAsync(commitId, containers, dbStructure, isReverse);
    }
    public async Task<ICollection<DatabaseItemContentCompare>> GetContentDiffsAsync(int commitId, DbStructureDto oldDbStructure, 
        bool isReverse = false)
    {
        var containers = await _blobStorageService.GetContainersByPrefixAsync(commitId.ToString());
        return await GetTextPairFromBlobsAsync(commitId, containers, oldDbStructure, isReverse);
    }

    private async Task<ICollection<DatabaseItemContentCompare>> GetTextPairFromBlobsAsync(int commitId, ICollection<string> containers, 
        DbStructureDto dbStructure, bool isReverse)
    {
        var differenceList = new List<DatabaseItemContentCompare>();
        var markedBlobIds = new List<string>();

        await CompareDbItemsContentAsync(dbStructure.DbTableStructures!, containers, commitId, DatabaseItemType.Table, differenceList, markedBlobIds, isReverse);

        foreach (var tableConstraints in dbStructure.DbConstraints!)
        {
            await CompareDbItemsContentAsync(tableConstraints.Constraints, containers, commitId, DatabaseItemType.Constraint, differenceList, markedBlobIds, isReverse);
        }
        
        await CompareDbItemsContentAsync(dbStructure.DbFunctionDetails!.Details, containers, commitId, DatabaseItemType.Function, differenceList, markedBlobIds, isReverse);
        await CompareDbItemsContentAsync(dbStructure.DbProcedureDetails!.Details, containers, commitId, DatabaseItemType.StoredProcedure, differenceList, markedBlobIds, isReverse);
        await CompareDbItemsContentAsync(dbStructure.DbViewsDetails!.Details, containers, commitId, DatabaseItemType.View, differenceList, markedBlobIds, isReverse);
        await CompareDbItemsContentAsync(dbStructure.DbUserDefinedDataTypeDetailsDto.Details, containers, commitId,
            DatabaseItemType.UserDefinedDataType, differenceList, markedBlobIds, isReverse);

        await CompareDbItemsContentAsync(dbStructure.DbUserDefinedTableTypeDetailsDto.Tables, containers, commitId, DatabaseItemType.UserDefinedTableType,
            differenceList, markedBlobIds, isReverse);
        
        await CompareUnmarkedBlobsContentAsync<TableStructureDto>(DatabaseItemType.Table, containers, commitId, differenceList, markedBlobIds, isReverse);
        
        await CompareUnmarkedConstraintBlobsContentAsync(DatabaseItemType.Constraint, containers, commitId, differenceList, markedBlobIds, isReverse);
        
        await CompareUnmarkedBlobsContentAsync<FunctionDetailInfo>(DatabaseItemType.Function, containers, commitId, differenceList, markedBlobIds, isReverse);
        
        await CompareUnmarkedBlobsContentAsync<ProcedureDetailInfo>(DatabaseItemType.StoredProcedure, containers, commitId, differenceList, markedBlobIds, isReverse);

        await CompareUnmarkedBlobsContentAsync<ViewDetailInfo>(DatabaseItemType.View, containers, commitId, differenceList, markedBlobIds, isReverse);

        await CompareUnmarkedBlobsContentAsync<UserDefinedDataTypeDetailInfo>(DatabaseItemType.UserDefinedDataType,
            containers, commitId, differenceList, markedBlobIds, isReverse);

        await CompareUnmarkedBlobsContentAsync<UserDefinedTableDetailsDto>(DatabaseItemType.UserDefinedTableType,
            containers, commitId, differenceList, markedBlobIds, isReverse);
        
        return differenceList;
    }

    private async Task CompareUnmarkedBlobsContentAsync<T>(DatabaseItemType itemType, ICollection<string> containers, int commitId, List<DatabaseItemContentCompare> differenceList,
        List<string> markedBlobIds, bool isReverse) where T : BaseDbItem
    {
        ICollection<Blob> blobs = new List<Blob>();
        var dbItemContainer = containers.FirstOrDefault(cont => cont == GetContainerName(commitId, itemType));
        if (dbItemContainer is not null)
        {
            blobs = await _blobStorageService.GetAllBlobsByContainerNameAsync(dbItemContainer);
        }
        var unmarkedBlobs = blobs.Where(blob => !markedBlobIds.Contains(blob.Id));
        foreach (var blob in unmarkedBlobs)
        {
            differenceList.Add(GetDbItemDifference<T>(blob.Content!, itemType, isReverse));
        }
    }

    private async Task CompareUnmarkedConstraintBlobsContentAsync(DatabaseItemType itemType, ICollection<string> containers, int commitId, 
        List<DatabaseItemContentCompare> differenceList, ICollection<string> markedBlobIds, bool isReverse)
    {
        ICollection<Blob> blobs = new List<Blob>();
        var dbItemContainer = containers.FirstOrDefault(cont => cont == GetContainerName(commitId, itemType));
        if (dbItemContainer is not null)
        {
            blobs = await _blobStorageService.GetAllBlobsByContainerNameAsync(dbItemContainer);
        }
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
        int commitId, DatabaseItemType itemType, ICollection<DatabaseItemContentCompare> differenceList, ICollection<string> markedBlobIds, 
        bool isReverse)
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
                differenceList.Add(GetDbItemDifference(Encoding.UTF8.GetBytes(""), dbItem, itemType, isReverse));
                continue;
            }
            differenceList.Add(GetDbItemDifference(currentBlob.Content!, dbItem, itemType, isReverse));
            markedBlobIds.Add(currentBlob.Id);
        }
    }

    private DatabaseItemContentCompare GetDbItemDifference<T>(byte[] blobContent, T dbItem,
        DatabaseItemType itemType, bool isReverse) where T : BaseDbItem
    {
        CheckBlockContentNotNull(blobContent);

        var commitItemContent = DeserializeBlobContent<T>(blobContent);
        TextPairRequestDto textPair;
        if (!isReverse)
        {
            textPair = new TextPairRequestDto
            {
                OldText = JsonConvert.SerializeObject(commitItemContent),
                NewText = JsonConvert.SerializeObject(dbItem),
                IgnoreWhitespace = true
            };
        }
        else
        {
            textPair = new TextPairRequestDto
            {
                OldText = JsonConvert.SerializeObject(dbItem),
                NewText = JsonConvert.SerializeObject(commitItemContent),
                IgnoreWhitespace = true
            };
        }

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

    private DatabaseItemContentCompare GetDbItemDifference<T>(byte[] blobContent, DatabaseItemType itemType, bool isReverse) 
        where T : BaseDbItem
    {
        CheckBlockContentNotNull(blobContent);
        
        var commitItemContent = DeserializeBlobContent<T>(blobContent);
        TextPairRequestDto textPair;
        if (!isReverse)
        {
            textPair = new TextPairRequestDto
            {
                OldText = JsonConvert.SerializeObject(commitItemContent),
                NewText = string.Empty,
                IgnoreWhitespace = true
            };
        }
        else
        {
            textPair = new TextPairRequestDto
            {
                OldText = string.Empty,
                NewText = JsonConvert.SerializeObject(commitItemContent),
                IgnoreWhitespace = true
            };
        }
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