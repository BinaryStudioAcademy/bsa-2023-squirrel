using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using Squirrel.AzureBlobStorage.Interfaces;
using Squirrel.Shared.Enums;
using Squirrel.Shared.DTO.DatabaseItem;
using Squirrel.Shared.DTO.Text;
using Squirrel.SqlService.BLL.Interfaces;
using Squirrel.SqlService.BLL.Models.DTO;
using System.Text;
using Squirrel.SqlService.BLL.Models.DTO.Abstract;
using Blob = Squirrel.AzureBlobStorage.Models.Blob;
using Squirrel.SqlService.BLL.Models.DTO.Function;
using Squirrel.SqlService.BLL.Models.DTO.Procedure;
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

    public async Task<List<DatabaseItemContentCompare>> GetInlineContentDiffsAsync(int commitId, Guid tempBlobId)
    {
        return await GetTextPairFromBlobs(commitId, tempBlobId);
    }

    public async Task<List<DatabaseItemContentCompare>> GetSideBySideContentDiffsAsync(int commitId, Guid tempBlobId)
    {
        return await GetTextPairFromBlobs(commitId, tempBlobId);
    }

    private async Task<List<DatabaseItemContentCompare>> GetTextPairFromBlobs(int commitId, Guid tempBlobId)
    {
        var containers = await _blobStorageService.GetContainersByPrefixAsync(commitId.ToString());
        var dbStructure = await GetTempBlobContentAsync(tempBlobId);
        var differenceList = new List<DatabaseItemContentCompare>();
        var markedBlobIds = new List<string>();

        await CompareDbItemsContent(dbStructure.TableStructures!, containers, commitId, DatabaseItemType.Table, differenceList, markedBlobIds);
        await CompareDbItemsContent(dbStructure.Constraints!.Constraints, containers, commitId, DatabaseItemType.Constraint, differenceList, markedBlobIds);
        await CompareDbItemsContent(dbStructure.FunctionDetails!.Details, containers, commitId, DatabaseItemType.Function, differenceList, markedBlobIds);
        await CompareDbItemsContent(dbStructure.ProcedureDetails!.Details, containers, commitId, DatabaseItemType.StoredProcedure, differenceList, markedBlobIds);

        var tableContainer = GetContainerName(commitId, DatabaseItemType.Table);
        await CompareRemovedDbItems(new TableStructureDto(), DatabaseItemType.Table, tableContainer, differenceList, markedBlobIds);
        
        var constraintContainer = GetContainerName(commitId, DatabaseItemType.Constraint);
        await CompareRemovedDbItems(new Constraint(), DatabaseItemType.Constraint, constraintContainer, differenceList, markedBlobIds);
        
        var functionContainer = GetContainerName(commitId, DatabaseItemType.Function);
        await CompareRemovedDbItems(new FunctionDetailInfo(), DatabaseItemType.Function, functionContainer, differenceList, markedBlobIds);
        
        var spContainer = GetContainerName(commitId, DatabaseItemType.StoredProcedure);
        await CompareRemovedDbItems(new ProcedureDetailInfo(), DatabaseItemType.StoredProcedure, spContainer, differenceList, markedBlobIds);

        return differenceList;
    }

    private async Task CompareRemovedDbItems<T>(T dbitem, DatabaseItemType itemType, string containerName, List<DatabaseItemContentCompare> differenceList,
        List<string> markedBlobIds) where T : BaseDbItem
    {
        var blobs = await _blobStorageService.GetAllBlobsByContainerNameAsync(containerName);
        var unmarkedBlobs = blobs.Where(blob => !markedBlobIds.Contains(blob.Id));
        foreach (var blob in unmarkedBlobs)
        {
            differenceList.Add(GetDbItemDifference(dbitem, blob.Content!, itemType));
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
        if (blobContent is null)
        {
            throw new Exception("Blob Content is empty");
        }

        var jsonString = Encoding.UTF8.GetString(blobContent);
        var commitItemContent = JsonConvert.DeserializeObject<T>(jsonString)!;

        var textPair = new TextPairRequestDto
        {
            //TODO with models - not json
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

    private DatabaseItemContentCompare GetDbItemDifference<T>(T dbItem, byte[] blobContent, DatabaseItemType itemType) where T: BaseDbItem
    {
        if (blobContent is null)
        {
            throw new Exception("Blob Content is empty");
        }

        var jsonString = Encoding.UTF8.GetString(blobContent);
        var commitItemContent = JsonConvert.DeserializeObject<T>(jsonString)!;

        var textPair = new TextPairRequestDto
        {
            //TODO with models - not json
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

    public async Task GenerateTempBlobContentAsync(int commitId)
    {
        DbStructureDto dbStructure = new DbStructureDto();
        var tableBlobs = await _blobStorageService.GetAllBlobsByContainerNameAsync($"{commitId}-table");
        var constBlobs = await _blobStorageService.GetAllBlobsByContainerNameAsync($"{commitId}-constraint");
        var spBlobs = await _blobStorageService.GetAllBlobsByContainerNameAsync($"{commitId}-storedprocedure");
        var funcBlobs = await _blobStorageService.GetAllBlobsByContainerNameAsync($"{commitId}-function");
        foreach (var blob in tableBlobs)
        {
            var jsonString = Encoding.UTF8.GetString(blob.Content);
            var content = JsonConvert.DeserializeObject<TableStructureDto>(jsonString)!;
            dbStructure.TableStructures.Add(content);
        }
        foreach (var blob in constBlobs)
        {
            var jsonString = Encoding.UTF8.GetString(blob.Content);
            var content = JsonConvert.DeserializeObject<Constraint>(jsonString)!;
            dbStructure.Constraints.Constraints.Add(content);
        }
        foreach (var blob in spBlobs)
        {
            var jsonString = Encoding.UTF8.GetString(blob.Content);
            var content = JsonConvert.DeserializeObject<ProcedureDetailInfo>(jsonString)!;
            dbStructure.ProcedureDetails.Details.Add(content);
        }
        foreach (var blob in funcBlobs)
        {
            var jsonString = Encoding.UTF8.GetString(blob.Content);
            var content = JsonConvert.DeserializeObject<FunctionDetailInfo>(jsonString)!;
            dbStructure.FunctionDetails.Details.Add(content);
        }
        var tempblob = new Blob
        {
            Id = $"{new Guid()}".ToLower(),
            ContentType = "application/json",
            Content = Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(dbStructure)),
        };
        await _blobStorageService.UploadAsync(_configuration["UserDbChangesBlobContainerName"], tempblob);
    }
}
