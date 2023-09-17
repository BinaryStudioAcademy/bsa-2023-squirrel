using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using Squirrel.AzureBlobStorage.Interfaces;
using Squirrel.Shared.DTO.DatabaseItem;
using Squirrel.Shared.DTO.Text;
using Squirrel.SqlService.BLL.Interfaces;
using System.Text;

namespace Squirrel.SqlService.BLL.Services
{
    public class ContentDifferenceService: IContentDifferenceService
    {
        private readonly IBlobStorageService _blobStorageService;
        private readonly IConfiguration _configuration;
        private readonly ITextService _textService;
        private readonly ISqlFormatterService _sqlFormatterService;

        public ContentDifferenceService(IBlobStorageService blobStorageService, IConfiguration configuration, ITextService textService)
        {
            _blobStorageService = blobStorageService;
            _textService = textService;
            _configuration = configuration;
        }
        //TODO methods for functions/SPs
        public async Task<InLineDiffResultDto> GetInlineContentDiffsAsync(string commitBlobId, string tempBlobId)
        {
            return _textService.GetInlineDiffs(await GetTextPairFromBlobs(commitBlobId, tempBlobId));
        }

        public async Task<SideBySideDiffResultDto> GetSideBySideContentDiffsAsync(string commitBlobId, string tempBlobId)
        {
            return _textService.GetSideBySideDiffs(await GetTextPairFromBlobs(commitBlobId, tempBlobId));
        }

        private async Task<TextPairRequestDto> GetTextPairFromBlobs(string commitBlobId, string tempBlobId)
        {
            var latestCommitDbStructure = await GetCommitBlobContentAsync(commitBlobId);
            var latestDbStructure = await GetTempBlobContentAsync(tempBlobId);
            return new TextPairRequestDto
            {
                OldText = latestCommitDbStructure.ToList()[0].Name,
                NewText = latestDbStructure.ToList()[1].Name,
                IgnoreWhitespace = true
            };
        }

        //commitBlobId must be from CORE.DAL -> CommitFiles.BlobId
        private async Task<ICollection<DatabaseItem>> GetCommitBlobContentAsync(string commitBlobId)
        {
            var blob = await _blobStorageService.DownloadAsync(_configuration["UserDbCommitsBlobContainerName"], commitBlobId);
            if (blob.Content is not null)
            {
                var jsonString = Encoding.UTF8.GetString(blob.Content);
                return JsonConvert.DeserializeObject<ICollection<DatabaseItem>>(jsonString)!;
            }
            throw new Exception("Blob Content is empty");
        }

        private async Task<ICollection<DatabaseItem>> GetTempBlobContentAsync(string tempBlobId)
        {
            var blob = await _blobStorageService.DownloadAsync(_configuration["UserDbChangesBlobContainerName"], tempBlobId);
            if (blob.Content is not null)
            {
                var jsonString = Encoding.UTF8.GetString(blob.Content);
                return JsonConvert.DeserializeObject<ICollection<DatabaseItem>>(jsonString)!;
            }
            throw new Exception("Blob Content is empty");
        }
    }
}
