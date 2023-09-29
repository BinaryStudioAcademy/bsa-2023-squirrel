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

    public ContentDifferenceService(IBlobStorageService blobStorageService, IConfiguration configuration, ITextService textService)
    {
        _blobStorageService = blobStorageService;
        _textService = textService;
        _configuration = configuration;
    }

    public async Task<ICollection<DatabaseItemContentCompare>> GetContentDiffsAsync(int commitId, Guid tempBlobId)
    {
        return await GetTextPairFromBlobsAsync(commitId, tempBlobId);
    }

    public async Task<string> GetContentAsync(Guid blobId)
    {
        var blob = await _blobStorageService.DownloadAsync("user-db-changes", blobId.ToString());
        return Encoding.UTF8.GetString(blob.Content);
    }

    private async Task<ICollection<DatabaseItemContentCompare>> GetTextPairFromBlobsAsync(int commitId, Guid tempBlobId)
    {
        var containers = await _blobStorageService.GetContainersByPrefixAsync(commitId.ToString());
        var dbStructure = await GetTempBlobContentAsync(tempBlobId);
        var differenceList = new List<DatabaseItemContentCompare>();
        var markedBlobIds = new List<string>();

        await CompareDbItemsContentAsync(dbStructure.DbTableStructures!, containers, commitId, DatabaseItemType.Table, differenceList, markedBlobIds);
        //
        // foreach (var tableConstraints in dbStructure.DbConstraints!)
        // {
        //     await CompareDbItemsContentAsync(tableConstraints.Constraints, containers, commitId, DatabaseItemType.Constraint, differenceList, markedBlobIds);
        // }
        //
        
        await CompareDbItemsDefinitionAsync(dbStructure.DbFunctionDetails!.Details, containers, commitId, DatabaseItemType.Function, differenceList, markedBlobIds);
          
        await CompareDbItemsDefinitionAsync(dbStructure.DbProcedureDetails!.Details, containers, commitId, DatabaseItemType.StoredProcedure, differenceList, markedBlobIds);
        
        await CompareDbItemsDefinitionAsync(dbStructure.DbViewsDetails!.Details, containers, commitId, DatabaseItemType.View, differenceList, markedBlobIds);
        
         
         // await CompareDbItemsContentAsync(dbStructure.DbUserDefinedDataTypeDetailsDto.Details, containers, commitId,
         //     DatabaseItemType.UserDefinedDataType, differenceList, markedBlobIds);
         //
         // await CompareDbItemsContentAsync(dbStructure.DbUserDefinedTableTypeDetailsDto.Tables, containers, commitId, DatabaseItemType.UserDefinedTableType,
         //     differenceList, markedBlobIds);
         
         
         await CompareUnmarkedBlobsContentAsync<TableStructureDto>(DatabaseItemType.Table, containers, commitId, differenceList, markedBlobIds);
         //
         // await CompareUnmarkedConstraintBlobsContent(DatabaseItemType.Constraint, containers, commitId, differenceList, markedBlobIds);
        
        // !!!
         await CompareUnmarkedBlobsDefinitionAsync<FunctionDetailInfo>(DatabaseItemType.Function, containers, commitId, differenceList, markedBlobIds);
         
         await CompareUnmarkedBlobsDefinitionAsync<ProcedureDetailInfo>(DatabaseItemType.StoredProcedure, containers, commitId, differenceList, markedBlobIds);
         
         await CompareUnmarkedBlobsDefinitionAsync<ViewDetailInfo>(DatabaseItemType.View, containers, commitId, differenceList, markedBlobIds);
         
         // await CompareUnmarkedBlobsContentAsync<UserDefinedDataTypeDetailInfo>(DatabaseItemType.UserDefinedDataType, 
         //     containers, commitId, differenceList, markedBlobIds);
         //
         // await CompareUnmarkedBlobsContentAsync<UserDefinedTableDetailsDto>(DatabaseItemType.UserDefinedTableType,
         //     containers, commitId, differenceList, markedBlobIds);
         
        return differenceList;
    }
    
    private async Task CompareDbItemsContentAsync<T>(List<T> dbStructureItemCollection, ICollection<string> containers,
        int commitId, DatabaseItemType itemType, ICollection<DatabaseItemContentCompare> differenceList, 
        ICollection<string> markedBlobIds) where T : BaseDbItem
    {
        var blobs = await GetBlobsByContainerName(containers, commitId, itemType);
        
        foreach (var dbItem in dbStructureItemCollection)
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
    
     private async Task CompareDbItemsDefinitionAsync<T>(List<T> dbStructureItemCollection,
        ICollection<string> containers, int commitId, DatabaseItemType itemType,
        ICollection<DatabaseItemContentCompare> differenceList,
        ICollection<string> markedBlobIds) where T: BaseDbItemWithDefinition
     {
         var blobs = await GetBlobsByContainerName(containers, commitId, itemType);
        
        foreach (var dbItem in dbStructureItemCollection)
        {
            var currentBlob = blobs.FirstOrDefault(blob => blob.Id == GetBlobName(dbItem.Schema, dbItem.Name));
            if (currentBlob is null)
            {
                differenceList.Add(GetDbItemDefinitionDifference(Encoding.UTF8.GetBytes(""), dbItem, itemType));
                continue;
            }
            differenceList.Add(GetDbItemDefinitionDifference(currentBlob.Content!, dbItem, itemType));
            markedBlobIds.Add(currentBlob.Id);
        }
    }
    
     private async Task CompareUnmarkedBlobsContentAsync<T>(DatabaseItemType itemType, ICollection<string> containers, int commitId, List<DatabaseItemContentCompare> differenceList,
        List<string> markedBlobIds) where T : BaseDbItem
    {
        var blobs = await GetBlobsByContainerName(containers, commitId, itemType);
        var unmarkedBlobs = blobs.Where(blob => !markedBlobIds.Contains(blob.Id));
        
        foreach (var blob in unmarkedBlobs)
        {
            differenceList.Add(GetDbItemDifference<T>(blob.Content!, itemType));
        }
    }
    
    private async Task CompareUnmarkedBlobsDefinitionAsync<T>(DatabaseItemType itemType, ICollection<string> containers, int commitId, List<DatabaseItemContentCompare> differenceList,
        List<string> markedBlobIds) where T : BaseDbItemWithDefinition
    {
        var blobs = await GetBlobsByContainerName(containers, commitId, itemType);
        var unmarkedBlobs = blobs.Where(blob => !markedBlobIds.Contains(blob.Id));
        
        foreach (var blob in unmarkedBlobs)
        {
            differenceList.Add(GetDbItemDefinitionDifference<T>(blob.Content!, itemType));
        }
    }

    private async Task CompareUnmarkedConstraintBlobsContent(DatabaseItemType itemType, ICollection<string> containers, int commitId, List<DatabaseItemContentCompare> differenceList,
        ICollection<string> markedBlobIds)
    {
        var blobs = await GetBlobsByContainerName(containers, commitId, itemType);
        var unmarkedBlobs = blobs.Where(blob => !markedBlobIds.Contains(blob.Id));
        
        foreach (var blob in unmarkedBlobs)
        {
            CheckBlockContentNotNull(blob.Content!);
            
            var jsonString = Encoding.UTF8.GetString(blob.Content!);
            var tableConstraintsDto = JsonConvert.DeserializeObject<TableConstraintsDto>(jsonString)!;
            
            foreach (var constraint in tableConstraintsDto.Constraints)
            {
                var textPair = GetTextPairRequestDtoInstance(JsonConvert.SerializeObject(constraint),
                    string.Empty);
                
                differenceList.Add(GetDataBaseItemContentCompareInstance(constraint.Schema, constraint.Name, itemType,
                    textPair));
            }
        }
    }

    private DatabaseItemContentCompare GetDbItemDifference<T>(byte[] blobContent, T dbItem,
        DatabaseItemType itemType) where T : BaseDbItem
    {
        CheckBlockContentNotNull(blobContent);

        var commitItemContent = DeserializeBlobContent<T>(blobContent);
        var textPair = GetTextPairRequestDtoInstance(JsonConvert.SerializeObject(commitItemContent),
            JsonConvert.SerializeObject(dbItem));

        return GetDataBaseItemContentCompareInstance(dbItem.Schema, dbItem.Name, itemType, textPair);
    }
    
    private DatabaseItemContentCompare GetDbItemDifference<T>(byte[] blobContent, DatabaseItemType itemType) 
        where T : BaseDbItem
    {
        CheckBlockContentNotNull(blobContent);
        
        var commitItemContent = DeserializeBlobContent<T>(blobContent);
        var textPair = GetTextPairRequestDtoInstance(JsonConvert.SerializeObject(commitItemContent), 
            string.Empty);

        return GetDataBaseItemContentCompareInstance(commitItemContent.Schema, commitItemContent.Name, 
            itemType, textPair);
    }
    
    private DatabaseItemContentCompare GetDbItemDefinitionDifference<T>(byte[] blobContent, T dbItem,
        DatabaseItemType itemType) where T: BaseDbItemWithDefinition
    {
        CheckBlockContentNotNull(blobContent);

        var commitItemContent = DeserializeBlobContent<T>(blobContent);
        var textPair = GetTextPairRequestDtoInstance(commitItemContent?.Definition, dbItem.Definition);

        return GetDataBaseItemContentCompareInstance(dbItem.Schema, dbItem.Name, itemType, textPair);
    }

    private DatabaseItemContentCompare GetDbItemDefinitionDifference<T>(byte[] blobContent, DatabaseItemType itemType) 
        where T : BaseDbItemWithDefinition
    {
        CheckBlockContentNotNull(blobContent);
        
        var commitItemContent = DeserializeBlobContent<T>(blobContent);
        var textPair = GetTextPairRequestDtoInstance(commitItemContent.Definition, string.Empty);

        return GetDataBaseItemContentCompareInstance(commitItemContent.Schema, commitItemContent.Name, 
            itemType, textPair);
    }

    private DatabaseItemContentCompare GetDataBaseItemContentCompareInstance(string schema, string name,
        DatabaseItemType itemType, TextPairRequestDto textPair)
    {
        return new DatabaseItemContentCompare
        {
            SchemaName = schema,
            ItemName = name,
            ItemType = itemType,
            InLineDiff = _textService.GetInlineDiffs(textPair),
            SideBySideDiff = _textService.GetSideBySideDiffs(textPair),
        };
    }

    private TextPairRequestDto GetTextPairRequestDtoInstance(string? oldText, string? newText)
    {
        return new TextPairRequestDto
        {
            OldText = ConvertToMultiLine(oldText),
            NewText = ConvertToMultiLine(newText),
            IgnoreWhitespace = true
        };
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
    private async Task<ICollection<Blob>> GetBlobsByContainerName(ICollection<string> containers, int commitId,
        DatabaseItemType itemType)
    {
        var dbItemContainer = containers.FirstOrDefault(cont => cont == GetContainerName(commitId, itemType));
         
        if (dbItemContainer is null)
        {
            return new List<Blob>();
        }

        return await _blobStorageService.GetAllBlobsByContainerNameAsync(dbItemContainer);
    }

    private string ConvertToMultiLine(string? text)
    {
        if (text is null)
        {
            return string.Empty;
        }
        
        return text.Replace(@",""", ",\n\"")
            .Replace(@"},{", "}\n{")
            .Replace(@"\r", "\r")
            // .Replace(@"\t", "\t")
            .Replace(@"\n", "\n");
    }
    
    private string GetContainerName(int commitId, DatabaseItemType itemType) => $"{commitId}-{itemType}".ToLower();

    private string GetBlobName(string schema, string name) => $"{schema}-{name}".ToLower();
}