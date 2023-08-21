using AutoMapper;
using Azure.Storage.Blobs;
using Azure.Storage.Blobs.Models;
using Squirrel.Core.BLL.Interfaces;
using Squirrel.Core.Common.DTO.Blob;
using Squirrel.Core.DAL.Context;

namespace Squirrel.Core.BLL.Services
{
    public class AzureStorageService : BaseService, IStorageService
    {
        private readonly BlobServiceClient _blobServiceClient;

        public AzureStorageService(SquirrelCoreContext context, IMapper mapper, BlobServiceClient blobServiceClient) : base(context, mapper)
        {
            _blobServiceClient = blobServiceClient;
        }

        public async Task DeleteAsync(string container, string id)
        {
            var blobClient = await GetBlobClientInternalAsync(container, id);

            if (!await blobClient.ExistsAsync())
            {
                throw new InvalidOperationException($"Blob with id:{id} does not exist.");
            }
            await blobClient.DeleteAsync();
        }

        public async Task<BlobDto> DownloadAsync(string container, string id)
        {
            var blobClient = await GetBlobClientInternalAsync(container, id);

            if (await blobClient.ExistsAsync())
            {
                var content = await blobClient.DownloadContentAsync();

                BlobDto blob = new BlobDto
                {
                    Id = id,
                    Content = content.Value.Content.ToArray(),
                    ContentType = content.Value.Details.ContentType,
                    AbsoluteUri = blobClient.Uri.AbsoluteUri
                };

                return blob;
            }
            throw new InvalidOperationException($"Blob with id:{id} does not exist.");
        }

        public async Task<BlobDto> UpdateAsync(string container, BlobDto blob)
        {
            var blobClient = await GetBlobClientInternalAsync(container, blob.Id);

            if (await blobClient.ExistsAsync())
            {
                var blobHttpHeader = new BlobHttpHeaders { ContentType = blob.ContentType };
                await blobClient.UploadAsync(new BinaryData(blob.Content ?? new byte[] {}), 
                    new BlobUploadOptions { HttpHeaders = blobHttpHeader });

                return blob;
            }
            throw new InvalidOperationException($"Blob with id:{blob.Id} does not exist.");
        }

        public async Task<BlobDto> UploadAsync(string container, BlobDto blob)
        {
            var blobClient = await GetBlobClientInternalAsync(container, blob.Id);
            
            if (await blobClient.ExistsAsync())
            {
                throw new InvalidOperationException($"Blob with id:{blob.Id} already exists.");
            }

            var blobHttpHeader = new BlobHttpHeaders { ContentType = blob.ContentType };
            await blobClient.UploadAsync(new BinaryData(blob.Content ?? new byte[] {}), 
                new BlobUploadOptions { HttpHeaders = blobHttpHeader });

            blob.AbsoluteUri = blobClient.Uri.AbsoluteUri;
            return blob;
        }

        private async Task<BlobContainerClient> GetOrCreateContainerByNameAsync(string name)
        {
            var containerClient = _blobServiceClient.GetBlobContainerClient(name);
            await containerClient.CreateIfNotExistsAsync();

            return containerClient;
        }

        private async Task<BlobClient> GetBlobClientInternalAsync(string containerName, string blobName)
        {
            var container = await GetOrCreateContainerByNameAsync(containerName);
            return container.GetBlobClient(blobName);
        }

    }
}
