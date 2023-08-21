using Squirrel.Core.Common.DTO.Blob;

namespace Squirrel.Core.BLL.Interfaces
{
    public interface IStorageService
    {
        Task<BlobDto> UploadAsync(string container, BlobDto blob);
        Task<BlobDto> UpdateAsync(string container, BlobDto blob);
        Task<BlobDto> DownloadAsync(string container, string id);
        Task DeleteAsync(string container, string id);
    }
}
