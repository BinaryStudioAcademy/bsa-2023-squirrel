using Bogus.DataSets;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using Squirrel.AzureBlobStorage.Interfaces;
using Squirrel.AzureBlobStorage.Models;
using Squirrel.Core.DAL.Entities;
using Squirrel.Shared.DTO.SelectedItems;
using Squirrel.Shared.Enums;
using Squirrel.SqlService.BLL.Interfaces;
using Squirrel.SqlService.BLL.Models.DTO;
using Squirrel.SqlService.BLL.Models.DTO.Function;
using Squirrel.SqlService.BLL.Models.DTO.Procedure;
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
        foreach (var section in selectedItems.SelectedItems)
        {
            var children = section.Children.Where(x => x.Selected == true);
            foreach (var child in children)
            {
                if (section.Name == "Functions" && staged.DbFunctionDetails != null)
                {
                    var function = staged.DbFunctionDetails.Details.FirstOrDefault(x => x.Name == child.Name);
                    if (function != null)
                    {
                        var blobContent = Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(function));
                        await SaveToBlob($"{selectedItems.CommitId}-functions", $"{function.Schema}-{function.Name}".ToLower(), blobContent);
                    }
                }
                else if (section.Name == "Stored Procedures" && staged.DbProcedureDetails != null)
                {
                    var procedure = staged.DbProcedureDetails.Details.FirstOrDefault(x => x.Name == child.Name);
                    if (procedure != null)
                    {
                        var blobContent = Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(procedure));
                        await SaveToBlob($"{selectedItems.CommitId}-procedures", $"{procedure.Schema}-{procedure.Name}".ToLower(), blobContent);
                    }
                }
                else if (section.Name == "Constraints" && staged.DbConstraints != null)
                {
                    var constraint = staged.DbConstraints.Constraints.FirstOrDefault(x => x.Name == child.Name);
                    if(constraint != null)
                    {
                        // save to mongo
                    }
                }
                else if (section.Name == "Tables" && staged.DbTableStructures != null)
                {
                    var table = staged.DbTableStructures.FirstOrDefault(x => x.Name == child.Name);
                    if(table != null)
                    {
                        // save to mongo
                    }
                }
                // TO DO OTHER ENTITIES
            }
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
