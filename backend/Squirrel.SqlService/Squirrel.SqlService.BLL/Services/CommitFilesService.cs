using System.Text;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using Squirrel.AzureBlobStorage.Interfaces;
using Squirrel.AzureBlobStorage.Models;
using Squirrel.Shared.DTO;
using Squirrel.Shared.DTO.CommitFile;
using Squirrel.Shared.DTO.SelectedItems;
using Squirrel.Shared.Enums;
using Squirrel.SqlService.BLL.Interfaces;

namespace Squirrel.SqlService.BLL.Services;

public class CommitFilesService : ICommitFilesService
{
    private const string UserDbChangesBlobContainerNameSection = "UserDbChangesBlobContainerName";
    private readonly string _containerName;
    private readonly IBlobStorageService _blobService;

    public CommitFilesService(IBlobStorageService blobService, IConfiguration configuration)
    {
        _blobService = blobService;
        _containerName = configuration.GetRequiredSection(UserDbChangesBlobContainerNameSection).Value;
    }

    public async Task<ICollection<CommitFileDto>> SaveSelectedFilesAsync(SelectedItemsDto selectedItems)
    {
        var staged = await GetStagedAsync(selectedItems.Guid);
        var saved = new List<CommitFileDto>();
        foreach (var selected in selectedItems.StoredProcedures)
        {
            var item = staged.DbProcedureDetails?.Details.FirstOrDefault(x => x.Name == selected);
            var file = await SaveFileAsync(item, selectedItems.CommitId, DatabaseItemType.StoredProcedure);
            MarkAsSaved(saved, file);
        }
        foreach (var selected in selectedItems.Functions)
        {
            var item = staged.DbFunctionDetails?.Details.FirstOrDefault(x => x.Name == selected);
            var file = await SaveFileAsync(item, selectedItems.CommitId, DatabaseItemType.Function);
            MarkAsSaved(saved, file);
        }
        foreach (var selected in selectedItems.Tables)
        {
            var item = staged.DbTableStructures?.FirstOrDefault(x => x.Name == selected);
            var file = await SaveFileAsync(item, selectedItems.CommitId, DatabaseItemType.Table);
            MarkAsSaved(saved, file);
        }
        foreach (var selected in selectedItems.Constraints)
        {
            var item = staged.DbConstraints.FirstOrDefault(x => x.Name == selected);
            var file = await SaveFileAsync(item, selectedItems.CommitId, DatabaseItemType.Constraint);
            MarkAsSaved(saved, file);
        }
        foreach (var selected in selectedItems.Views)
        {
            var item = staged.DbViewsDetails.Details.FirstOrDefault(x => x.Name == selected);
            var file = await SaveFileAsync(item, selectedItems.CommitId, DatabaseItemType.View);
            MarkAsSaved(saved, file);
        }
        foreach (var selected in selectedItems.Types)
        {
            // TODO
        }

        return saved;
    }

    private void MarkAsSaved(List<CommitFileDto> saved, CommitFileDto? file)
    {
        if (file is not null)
        {
            saved.Add(file);
        }
    }

    private async Task<CommitFileDto?> SaveFileAsync<T>(T? item, int commitId, DatabaseItemType type) where T : BaseDbItem
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
        await SaveToBlobAsync(file, commitId, blobContent);

        return file;
    }

    private async Task SaveToBlobAsync(CommitFileDto dto, int commitId, byte[] content)
    {
        var blob = new Blob
        {
            Id = dto.BlobId,
            ContentType = "application/json",
            Content = content
        };
        await _blobService.UploadAsync($"{commitId}-{dto.FileType}".ToLower(), blob);
    }

    private async Task<DbStructureDto> GetStagedAsync(string changesGuid)
    {
        var blob = await _blobService.DownloadAsync(_containerName, changesGuid);
        var json = Encoding.UTF8.GetString(blob.Content ?? throw new ArgumentNullException());
        
        return JsonConvert.DeserializeObject<DbStructureDto>(json) ?? throw new JsonException($"{nameof(DbStructureDto)} deserialized as null!");
    }
}
