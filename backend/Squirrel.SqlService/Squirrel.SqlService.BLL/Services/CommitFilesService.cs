using Bogus.DataSets;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using Squirrel.AzureBlobStorage.Interfaces;
using Squirrel.AzureBlobStorage.Models;
using Squirrel.Shared.DTO.CommitFile;
using Squirrel.Shared.DTO.SelectedItems;
using Squirrel.SqlService.BLL.Interfaces;
using Squirrel.SqlService.BLL.Models.DTO;
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

    public async Task<ICollection<CommitFileDto>> SaveSelectedFiles(SelectedItemsDto selectedItems)
    {
        var staged = await GetStagedAsync(selectedItems.Guid);
        var saved = new List<CommitFileDto>();
        foreach (var selected in selectedItems.StoredProcedures) 
        {
            var item = staged.DbProcedureDetails?.Details.FirstOrDefault(x => x.Name == selected);
            if (item != null) 
            {
                var blobContent = Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(item));
                var file = new CommitFileDto
                {
                    BlobId = $"{item.Schema}-{item.Name}".ToLower(),
                    FileName = item.Name,
                    FileType = Shared.Enums.DatabaseItemType.StoredProcedure
                };
                await SaveToBlob(file, selectedItems.CommitId, blobContent);
                saved.Add(file);
            }
        }
        foreach (var selected in selectedItems.Functions)
        {
            var item = staged.DbFunctionDetails?.Details.FirstOrDefault(x => x.Name == selected);
            if (item != null)
            {
                var blobContent = Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(item));
                var file = new CommitFileDto
                {
                    BlobId = $"{item.Schema}-{item.Name}".ToLower(),
                    FileName = item.Name,
                    FileType = Shared.Enums.DatabaseItemType.Function
                };
                await SaveToBlob(file, selectedItems.CommitId, blobContent);
                saved.Add(file);
            }
        }
        foreach (var selected in selectedItems.Tables)
        {
            var item = staged.DbTableStructures?.FirstOrDefault(x => x.Name == selected);
            if (item != null)
            {
                var blobContent = Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(item));
                var file = new CommitFileDto
                {
                    BlobId = $"{item.Schema}-{item.Name}".ToLower(),
                    FileName = item.Name,
                    FileType = Shared.Enums.DatabaseItemType.Table
                };
                await SaveToBlob(file, selectedItems.CommitId, blobContent);
                saved.Add(file);
            }
        }
        foreach (var selected in selectedItems.Constraints)
        {
            var item = staged.DbConstraints?.Constraints.FirstOrDefault(x => x.Name == selected);
            if (item != null)
            {
                var blobContent = Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(item));
                var file = new CommitFileDto
                {
                    BlobId = $"{item.Schema}-{item.Name}".ToLower(),
                    FileName = item.Name,
                    FileType = Shared.Enums.DatabaseItemType.Constraint
                };
                await SaveToBlob(file, selectedItems.CommitId, blobContent);
                saved.Add(file);
            }
        }
        foreach (var selected in selectedItems.Types)
        {
            // TODO
        }

        return saved;
    }

    private async Task SaveToBlob(CommitFileDto dto, int commitId, byte[] content)
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
        
        return JsonConvert.DeserializeObject<DbStructureDto>(json);
    }

    public DbStructureDto GetTestStructure()
    {
        var constraints = new List<Constraint>()
        {
            new Constraint 
            { 
                CheckClause = "test",
                Columns = "test1",
                ConstraintName = "test",
                Name = "contraint1",
                Schema = "test"
            }
            ,
            new Constraint
            {
                CheckClause = "test",
                Columns = "test1",
                ConstraintName = "test",
                Name = "contraint2",
                Schema = "test"
            }
        };
        var procedures = new List<ProcedureDetailInfo>()
        {
            new ProcedureDetailInfo
            {
                Name = "procedure1",
                Definition = "test",
                Schema = "test"
            }
        };
        return new DbStructureDto
        {
            DbConstraints = new TableConstraintsDto { Constraints = constraints },
            DbProcedureDetails = new ProcedureDetailsDto { Details = procedures }
        };
    }
}
