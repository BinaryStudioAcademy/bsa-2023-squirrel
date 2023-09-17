using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using Squirrel.AzureBlobStorage.Interfaces;
using Squirrel.AzureBlobStorage.Models;
using Squirrel.Shared.Enums;
using Squirrel.Shared.DTO.DatabaseItem;
using Squirrel.Shared.DTO.Text;
using Squirrel.SqlService.BLL.Interfaces;
using Squirrel.SqlService.BLL.Models.DTO;
using System.Text;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Squirrel.SqlService.BLL.Models.DTO.Abstract;

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
        var commitBlobs = GetBlobsByCommitId(commitId);
        var dbStructure = await GetTempBlobContentAsync(tempBlobId);
        var differenceList = new List<DatabaseItemContentCompare>();
        foreach (var table in dbStructure.TableStructures)
        {
            var blobIdSuffix = $"Table_{table.Schema}_{table.Name}";
            differenceList.Add(await GetDbItemDifference(commitBlobs, table, blobIdSuffix, DatabaseItemType.Table));
        }
        var tableConstraintsDto = dbStructure.Constraints;
        //foreach (var constraint in tableConstraintsDto.Constraints)
        //{
        //    var blobIdSuffix = $"Constraint_{tableConstraintsDto.Schema}_{tableConstraintsDto.Name}";
        //    differenceList.Add(await GetDbItemDifference(commitBlobs, constraint, blobIdSuffix, DatabaseItemType.Constraint));
        //}
        foreach (var procedure in dbStructure.ProcedureDetails.Details)
        {
            var blobIdSuffix = $"StoredProcedure_{procedure.Schema}_{procedure.Name}";
            differenceList.Add(await GetDbItemDifference(commitBlobs, procedure, blobIdSuffix, DatabaseItemType.StoredProcedure));
        }
        foreach (var function in dbStructure.FunctionDetails.Details)
        {
            var blobIdSuffix = $"Function_{function.Schema}_{function.Name}";
            differenceList.Add(await GetDbItemDifference(commitBlobs, function, blobIdSuffix, DatabaseItemType.Function));
        }
        return differenceList;
    }

    private async Task<DatabaseItemContentCompare> GetDbItemDifference<T>(List<string> commitBlobs, T dbItem, string blobIdSuffix, DatabaseItemType itemType) where T : BaseDbItem
    {
        var commitItemContent = await GetCommitBlobContentAsync<T>(commitBlobs.FirstOrDefault(
            blobId => blobId.Contains(blobIdSuffix)) ?? string.Empty);
        var diff = new TextPairRequestDto
        {
            OldText = JsonConvert.SerializeObject(commitItemContent),
            NewText = JsonConvert.SerializeObject(dbItem),
            IgnoreWhitespace = true
        };
        var dbItemContentCompare = new DatabaseItemContentCompare
        {
            SchemaName = dbItem.Schema,
            ItemName = dbItem.Name,
            ItemType = DatabaseItemType.Table,
            InLineDiff = _textService.GetInlineDiffs(diff),
            SideBySideDiff = _textService.GetSideBySideDiffs(diff),
        };
        return dbItemContentCompare;
    }

    private List<string> GetBlobsByCommitId(int commitId)
    {
        // TODO: get commitId and objectType and objectName
        // projectId_commitId_objectType_objectName
        var blobIds = new List<string>();
        blobIds.Add("15_10_Function_Test1");
        blobIds.Add("15_10_Function_Test2");
        return blobIds;
    }
    private async Task Test(int commitId)
    {
        // TODO: get commitId and objectType and objectName
        // projectId_commitId_objectType_objectName
        var sql1 = @"CREATE FUNCTION east_or_west (   @long DECIMAL(9,6)  )  RETURNS VARCHAR(36) AS  BEGIN   DECLARE @return_value CHAR(4);   SET @return_value = 'same';      IF (@long > 0.00) SET @return_value = 'east';      IF (@long < 0.00) SET @return_value = 'west';         RETURN @return_value  END;";
        var sql2 = @"   CREATE FUNCTION dbo.fn_diagramobjects()    RETURNS int   WITH EXECUTE AS N'dbo'   AS   BEGIN    declare @id_upgraddiagrams  int    declare @id_sysdiagrams   int    declare @id_helpdiagrams  int    declare @id_helpdiagramdefinition int    declare @id_creatediagram int    declare @id_renamediagram int    declare @id_alterdiagram  int     declare @id_dropdiagram  int    declare @InstalledObjects int      select @InstalledObjects = 0      select  @id_upgraddiagrams = object_id(N'dbo.sp_upgraddiagrams'),     @id_sysdiagrams = object_id(N'dbo.sysdiagrams'),     @id_helpdiagrams = object_id(N'dbo.sp_helpdiagrams'),     @id_helpdiagramdefinition = object_id(N'dbo.sp_helpdiagramdefinition'),     @id_creatediagram = object_id(N'dbo.sp_creatediagram'),     @id_renamediagram = object_id(N'dbo.sp_renamediagram'),     @id_alterdiagram = object_id(N'dbo.sp_alterdiagram'),      @id_dropdiagram = object_id(N'dbo.sp_dropdiagram')      if @id_upgraddiagrams is not null     select @InstalledObjects = @InstalledObjects + 1    if @id_sysdiagrams is not null     select @InstalledObjects = @InstalledObjects + 2    if @id_helpdiagrams is not null     select @InstalledObjects = @InstalledObjects + 4    if @id_helpdiagramdefinition is not null     select @InstalledObjects = @InstalledObjects + 8    if @id_creatediagram is not null     select @InstalledObjects = @InstalledObjects + 16    if @id_renamediagram is not null     select @InstalledObjects = @InstalledObjects + 32    if @id_alterdiagram  is not null     select @InstalledObjects = @InstalledObjects + 64    if @id_dropdiagram is not null     select @InstalledObjects = @InstalledObjects + 128        return @InstalledObjects    END   ";
        var blob1 = new Blob
        {
            Id = "15_10_Function_Test1",
            ContentType = "application/json",
            Content = Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(sql1)),
        };
        var blob2 = new Blob
        {
            Id = "15_10_Function_Test2",
            ContentType = "application/json",
            Content = Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(sql2)),
        };
        await _blobStorageService.UploadAsync(_configuration["UserDbCommitsBlobContainerName"], blob2);
    }

    //commitBlobId must be from CORE.DAL -> CommitFiles.BlobId
    private async Task<T> GetCommitBlobContentAsync<T>(string commitBlobId)
    {
        var blob = await _blobStorageService.DownloadAsync(_configuration["UserDbCommitsBlobContainerName"], commitBlobId);
        if (blob.Content is not null)
        {
            var jsonString = Encoding.UTF8.GetString(blob.Content);
            return JsonConvert.DeserializeObject<T>(jsonString)!;
        }
        throw new Exception("Blob Content is empty");
    }

    private async Task<DbStructureDto> GetTempBlobContentAsync(Guid tempBlobId)
    {
        var blob = await _blobStorageService.DownloadAsync(_configuration["UserDbChangesBlobContainerName"], tempBlobId.ToString());
        if (blob?.Content is not null)
        {
            var jsonString = Encoding.UTF8.GetString(blob.Content);
            return JsonConvert.DeserializeObject<DbStructureDto>(jsonString)!;
        }
        throw new Exception("Blob Content is empty");
    }
}
