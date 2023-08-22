using Squirrel.AzureBlobStorage.WebApi.Models;

namespace Squirrel.AzureBlobStorage.WebApi.Interfaces
{
    public interface IBlobStorageService
    {
        Task<Blob> UploadAsync(string containerName, Blob blob);
        Task<Blob> UpdateAsync(string containerName, Blob blob);
        Task<Blob> DownloadAsync(string containerName, string blobId);
        Task DeleteAsync(string containerName, string blobId);
    }
}
