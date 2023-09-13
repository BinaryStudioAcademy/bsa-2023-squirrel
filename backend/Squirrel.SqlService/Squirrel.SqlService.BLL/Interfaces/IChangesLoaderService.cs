using Squirrel.AzureBlobStorage.Models;

namespace Squirrel.SqlService.BLL.Interfaces;

public interface IChangesLoaderService
{
    Task<Blob> LoadChangesToBlobAsync(Guid changeId);
}

