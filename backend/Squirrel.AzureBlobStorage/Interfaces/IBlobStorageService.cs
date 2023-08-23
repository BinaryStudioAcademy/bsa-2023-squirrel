using Squirrel.AzureBlobStorage.Models;

namespace Squirrel.AzureBlobStorage.Interfaces;

public interface IBlobStorageService
{
    Task<Blob> UploadAsync(string containerName, Blob blob);
    Task<Blob> UpdateAsync(string containerName, Blob blob);
    Task<Blob> DownloadAsync(string containerName, string blobId);
    Task<bool> DeleteAsync(string containerName, string blobId);
}