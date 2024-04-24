using System.Text;
using DiffPlex.DiffBuilder.Model;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using Squirrel.AzureBlobStorage.Interfaces;
using Squirrel.AzureBlobStorage.Models;
using Squirrel.Core.DAL.Context;
using Squirrel.Shared.DTO;
using Squirrel.Shared.DTO.CommitFile;
using Squirrel.Shared.DTO.DatabaseItem;
using Squirrel.Shared.DTO.SelectedItems;
using Squirrel.Shared.Enums;
using Squirrel.SqlService.BLL.Interfaces;

namespace Squirrel.SqlService.BLL.Services;

public class CommitFilesService : ICommitFilesService
{
    private const string UserDbChangesBlobContainerNameSection = "UserDbChangesBlobContainerName";
    private readonly string _containerName;
    private readonly IBlobStorageService _blobService;
    private readonly IContentDifferenceService _contentDifferenceService;

    public CommitFilesService(IBlobStorageService blobService, IContentDifferenceService contentDifferenceService, 
        IConfiguration configuration)
    {
        _blobService = blobService;
        _contentDifferenceService = contentDifferenceService;
        _containerName = configuration.GetRequiredSection(UserDbChangesBlobContainerNameSection).Value;
    }

    public async Task<ICollection<CommitFileDto>> SaveSelectedFilesAsync(SelectedItemsDto selectedItems)
    {
        var staged = await GetStagedAsync(selectedItems.Guid);
        var saved = new List<CommitFileDto>();

        ICollection<DatabaseItemContentCompare> contentDifferenceList = new List<DatabaseItemContentCompare>(); 
        if (selectedItems.PreviousHeadCommitId != 0)
        {
            await ApplyParentCommitChanges(selectedItems.CommitId, selectedItems.PreviousHeadCommitId);
            contentDifferenceList = await _contentDifferenceService
                .GetContentDiffsAsync(selectedItems.CommitId, Guid.Parse(selectedItems.Guid));
        }
        
        foreach (var selected in selectedItems.StoredProcedures)
        {
            var item = staged.DbProcedureDetails?.Details.FirstOrDefault(x => x.Name == selected);
            if (item == null)
            {
                await DeleteFileAsync(selected, selectedItems.CommitId,  DatabaseItemType.StoredProcedure);
                break;
            }
            await ApplyCommitChangesToFile(item, selectedItems, contentDifferenceList, DatabaseItemType.StoredProcedure, saved);
        }
        foreach (var selected in selectedItems.Functions)
        {
            var item = staged.DbFunctionDetails?.Details.FirstOrDefault(x => x.Name == selected);
            if (item == null)
            {
                await DeleteFileAsync(selected, selectedItems.CommitId,  DatabaseItemType.Function);
                break;
            }
            await ApplyCommitChangesToFile(item, selectedItems, contentDifferenceList, DatabaseItemType.Function, saved);
        }
        foreach (var selected in selectedItems.Tables)
        {
            var item = staged.DbTableStructures?.FirstOrDefault(x => x.Name == selected);
            if (item == null)
            {
                await DeleteFileAsync(selected, selectedItems.CommitId,  DatabaseItemType.Table);
                break;
            }
            await ApplyCommitChangesToFile(item, selectedItems, contentDifferenceList, DatabaseItemType.Table, saved);
        }
        foreach (var selected in selectedItems.Constraints)
        {
            var item = staged.DbConstraints.FirstOrDefault(x => x.Name == selected);
            if (item == null)
            {
                await DeleteFileAsync(selected, selectedItems.CommitId,  DatabaseItemType.Constraint);
                break;
            }
            await ApplyCommitChangesToFile(item, selectedItems, contentDifferenceList, DatabaseItemType.Constraint, saved);
        }
        foreach (var selected in selectedItems.Views)
        {
            var item = staged.DbViewsDetails.Details.FirstOrDefault(x => x.Name == selected);
            if (item == null)
            {
                await DeleteFileAsync(selected, selectedItems.CommitId,  DatabaseItemType.View);
                break;
            }
            await ApplyCommitChangesToFile(item, selectedItems, contentDifferenceList, DatabaseItemType.View, saved);
        }
        foreach (var selected in selectedItems.Types)
        {
            // TODO
        }

        return saved;
    }

    private async Task ApplyCommitChangesToFile<T>(T? item, SelectedItemsDto selectedItems, 
        ICollection<DatabaseItemContentCompare> contentDifferenceList, DatabaseItemType dbType,
        List<CommitFileDto> saved) where T : BaseDbItem
    {
        foreach (var obj in contentDifferenceList)
        {
            if (obj.SchemaName == item!.Schema
                && obj.ItemName == item.Name
                && obj.ItemType == dbType)
            {
                var objStatus =  DbItemStatus.Modified;
                
                if (obj.InLineDiff!.DiffLinesResults.All(x => x.Type == ChangeType.Deleted))
                {
                    objStatus = DbItemStatus.Deleted;
                }
                if (obj.InLineDiff!.DiffLinesResults.All(x => x.Type == ChangeType.Inserted))
                {
                    objStatus = DbItemStatus.Created;
                }

                if (objStatus == DbItemStatus.Created)
                {
                    var file = await SaveFileAsync(item, selectedItems.CommitId, dbType, objStatus);
                    MarkAsSaved(saved, file);
                    return;
                }

                if (objStatus == DbItemStatus.Modified)
                {
                    var file = await SaveFileAsync(item, selectedItems.CommitId, dbType, objStatus);
                    MarkAsSaved(saved, file);
                    return;
                }

                return;
            }
        }
        
        var newFile = await SaveFileAsync(item, selectedItems.CommitId, dbType, DbItemStatus.Created);
        MarkAsSaved(saved, newFile);
    }

    private async Task ApplyParentCommitChanges(int commitId, int previousCommitId)
    {
        /// <summary>
        /// A method that writes changes from a specific commit to the current one.
        /// </summary>

        var containers = await _blobService.GetContainersByPrefixAsync(previousCommitId.ToString());
        foreach (var container in containers)
        {
           await _blobService.CopyContainerAsync(container,
                GetDestinationContainerName(commitId.ToString(), container));
        }
    }

    private string GetDestinationContainerName(string commitId, string sourceContainerName)
    {
        return commitId + "-" + sourceContainerName.Split('-')[1];
    }
    
    private void MarkAsSaved(List<CommitFileDto> saved, CommitFileDto? file)
    {
        if (file is not null)
        {
            saved.Add(file);
        }
    }

    private async Task<CommitFileDto?> SaveFileAsync<T>(T? item, int commitId, DatabaseItemType type,
        DbItemStatus status) where T : BaseDbItem
    {
        if (item is null)
        {
            return null;
        }

        var blobContent = Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(item));
        var file = new CommitFileDto
        {
            BlobId = $"{item.Schema}-{item.Name}".ToLower(),
            FileName = item.Name,
            FileType = type
        };
        await SaveToBlobAsync(file, commitId, blobContent, status);

        return file;
    }

    private async Task SaveToBlobAsync(CommitFileDto dto, int commitId, byte[] content, DbItemStatus status)
    {
        var blob = new Blob
        {
            Id = dto.BlobId,
            ContentType = "application/json",
            Content = content
        };

        if (status == DbItemStatus.Created)
        {
            await _blobService.UploadAsync($"{commitId}-{dto.FileType}".ToLower(), blob);    
        }
        
        if (status == DbItemStatus.Modified)
        {
            await _blobService.UpdateAsync($"{commitId}-{dto.FileType}".ToLower(), blob);    
        }
    }

    private async Task DeleteFileAsync(string itemName, int commitId, DatabaseItemType type)
    {
        await _blobService.DeleteAsync($"{commitId}-{type}".ToLower(), $"dbo-{itemName}".ToLower());
    }

    private async Task<DbStructureDto> GetStagedAsync(string changesGuid)
    {
        var blob = await _blobService.DownloadAsync(_containerName, changesGuid);
        var json = Encoding.UTF8.GetString(blob.Content ?? throw new ArgumentNullException());
        
        return JsonConvert.DeserializeObject<DbStructureDto>(json) ?? throw new JsonException($"{nameof(DbStructureDto)} deserialized as null!");
    }
}
