using Azure.Storage.Blobs.Models;
using Azure.Storage.Blobs;
using Squirrel.AzureBlobStorage.Interfaces;
using Squirrel.AzureBlobStorage.Models;

namespace Squirrel.AzureBlobStorage.Services;

public class AzureBlobStorageService : IBlobStorageService
{
    private readonly BlobServiceClient _blobServiceClient;

    public AzureBlobStorageService(BlobServiceClient blobServiceClient)
    {
        _blobServiceClient = blobServiceClient;
    }

    public async Task<Blob> UploadAsync(string containerName, Blob blob)
    {
        var blobClient = await GetBlobClientInternalAsync(containerName, blob.Id);

        if (await blobClient.ExistsAsync())
        {
            throw new InvalidOperationException($"Blob with id:{blob.Id} already exists.");
        }

        var blobHttpHeader = new BlobHttpHeaders { ContentType = blob.ContentType };
        await blobClient.UploadAsync(new BinaryData(blob.Content ?? new byte[] { }),
            new BlobUploadOptions { HttpHeaders = blobHttpHeader });

        return blob;
    }
    
    public async Task<string> UploadWithUrlAsync(string containerName, Blob blob)
    {
        var blobClient = await GetBlobClientInternalAsync(containerName, blob.Id);

        if (await blobClient.ExistsAsync())
        {
            throw new InvalidOperationException($"Blob with id:{blob.Id} already exists.");
        }

        var blobHttpHeader = new BlobHttpHeaders { ContentType = blob.ContentType };
        await blobClient.UploadAsync(new BinaryData(blob.Content ?? new byte[] { }),
            new BlobUploadOptions { HttpHeaders = blobHttpHeader });
        // TODO: add to private method, DRY
        return blobClient.Uri.AbsoluteUri;
    }

    public async Task<Blob> UpdateAsync(string containerName, Blob blob)
    {
        var blobClient = await GetBlobClientInternalAsync(containerName, blob.Id);

        if (!await blobClient.ExistsAsync())
        {
            throw new InvalidOperationException($"Blob with id:{blob.Id} does not exist.");
        }

        var blobHttpHeader = new BlobHttpHeaders { ContentType = blob.ContentType };
        await blobClient.UploadAsync(new BinaryData(blob.Content ?? new byte[] { }),
            new BlobUploadOptions { HttpHeaders = blobHttpHeader });

        return blob;
    }

    public async Task<Blob> DownloadAsync(string containerName, string blobId)
    {
        var blobClient = await GetBlobClientInternalAsync(containerName, blobId);

        if (!await blobClient.ExistsAsync())
        {
            throw new InvalidOperationException($"Blob with id:{blobId} does not exist.");
        }

        var content = await blobClient.DownloadContentAsync();
        Blob blob = new Blob
        {
            Id = blobId,
            Content = content.Value.Content.ToArray(),
            ContentType = content.Value.Details.ContentType,
        };

        return blob;
    }

    public async Task<bool> DeleteAsync(string containerName, string blobId)
    {
        var blobClient = await GetBlobClientInternalAsync(containerName, blobId);

        return await blobClient.DeleteIfExistsAsync();
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