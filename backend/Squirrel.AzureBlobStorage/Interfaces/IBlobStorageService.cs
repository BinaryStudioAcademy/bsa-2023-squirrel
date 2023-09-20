using Squirrel.AzureBlobStorage.Models;

namespace Squirrel.AzureBlobStorage.Interfaces;

public interface IBlobStorageService
{
    Task UploadAsync(string containerName, Blob blob);
    Task UpdateAsync(string containerName, Blob blob);
    Task<Blob> DownloadAsync(string containerName, string blobId);
    Task<bool> DeleteAsync(string containerName, string blobId);
    Task<ICollection<Blob>> GetAllBlobsByContainerNameAsync(string containerName);
    Task<ICollection<string>> GetContainersByPrefixAsync(string prefix);
}