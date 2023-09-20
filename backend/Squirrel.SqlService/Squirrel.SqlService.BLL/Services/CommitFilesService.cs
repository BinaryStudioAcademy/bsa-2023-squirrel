using Bogus.DataSets;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using Squirrel.AzureBlobStorage.Interfaces;
using Squirrel.AzureBlobStorage.Models;
using Squirrel.Shared.DTO.SelectedItems;
using Squirrel.SqlService.BLL.Interfaces;
using Squirrel.SqlService.BLL.Models.DTO;
using System.Text;

namespace Squirrel.SqlService.BLL.Services;
public class CommitFilesService : ICommitFilesService
{
    private readonly string _containerName;
    private readonly IBlobStorageService _blobService;

    public CommitFilesService(IBlobStorageService blobService, IConfiguration configuration)
    {
        _blobService = blobService;
        _containerName = configuration.GetRequiredSection("UserDbChangesBlobContainerName").Value;
    }

    public async Task SaveSelectedFiles(SelectedItemsDto selectedItems)
    {
        var staged = await GetStagedAsync(selectedItems.Guid);
        foreach (var selected in selectedItems.StoredProcedures) 
        {
            var item = staged.DbProcedureDetails?.Details.FirstOrDefault(x => x.Name == selected);
            if (item != null) 
            {
                var blobContent = Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(item));
                await SaveToBlob($"{selectedItems.CommitId}-procedures", $"{item.Schema}-{item.Name}".ToLower(), blobContent);
            }
        }
        foreach (var selected in selectedItems.Functions)
        {
            var item = staged.DbFunctionDetails?.Details.FirstOrDefault(x => x.Name == selected);
            if (item != null)
            {
                var blobContent = Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(item));
                await SaveToBlob($"{selectedItems.CommitId}-functions", $"{item.Schema}-{item.Name}".ToLower(), blobContent);
            }
        }
        foreach (var selected in selectedItems.Tables)
        {
            var item = staged.DbTableStructures?.FirstOrDefault(x => x.Name == selected);
            if (item != null)
            {
                // mongo
            }
        }
        foreach (var selected in selectedItems.Constraints)
        {
            var item = staged.DbConstraints?.Constraints.FirstOrDefault(x => x.Name == selected);
            if (item != null)
            {
                // mongo
            }
        }
        foreach (var selected in selectedItems.Types)
        {
            // TODO
        }
    }

    private async Task SaveToBlob(string containerName, string blobId, byte[] content)
    {
        var blob = new Blob
        {
            Id = blobId,
            ContentType = "application/json",
            Content = content
        };
        await _blobService.UploadAsync(containerName, blob);
    }

    private async Task<DbStructureDto> GetStagedAsync(string changesGuid)
    {
        var blob = await _blobService.DownloadAsync(_containerName, changesGuid);

        var json = Encoding.UTF8.GetString(blob.Content ?? throw new ArgumentNullException());
        
        return JsonConvert.DeserializeObject<DbStructureDto>(json);
    }
}
