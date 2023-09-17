using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using Squirrel.AzureBlobStorage.Interfaces;
using Squirrel.Shared.DTO.DatabaseItem;
using Squirrel.Shared.DTO.Text;
using Squirrel.SqlService.BLL.Interfaces;
using Squirrel.SqlService.BLL.Models.DTO;
using System.Text;

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
    //TODO methods for functions/SPs
    public async Task<InLineDiffResultDto> GetInlineContentDiffsAsync(int commitId, Guid tempBlobId)
    {
        return _textService.GetInlineDiffs(await GetTextPairFromBlobs(commitId, tempBlobId));
    }

    public async Task<SideBySideDiffResultDto> GetSideBySideContentDiffsAsync(int commitId, Guid tempBlobId)
    {
        return _textService.GetSideBySideDiffs(await GetTextPairFromBlobs(commitId, tempBlobId));
    }

    private async Task<TextPairRequestDto> GetTextPairFromBlobs(int commitBlobId, Guid tempBlobId)
    {
        var latestCommitDbStructure = await GetCommitBlobContentAsync(commitBlobId);
        var latestDbStructure = await GetTempBlobContentAsync(tempBlobId);
        return new TextPairRequestDto
        {
            OldText = latestCommitDbStructure.ToList()[0].Name,
            NewText = latestDbStructure.TableStructures[1].Name,
            IgnoreWhitespace = true
        };
    }

    //commitBlobId must be from CORE.DAL -> CommitFiles.BlobId
    private async Task<ICollection<DatabaseItem>> GetCommitBlobContentAsync(int commitBlobId)
    {
        var blob = await _blobStorageService.DownloadAsync(_configuration["UserDbCommitsBlobContainerName"], commitBlobId.ToString());
        if (blob.Content is not null)
        {
            var jsonString = Encoding.UTF8.GetString(blob.Content);
            return JsonConvert.DeserializeObject<ICollection<DatabaseItem>>(jsonString)!;
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
