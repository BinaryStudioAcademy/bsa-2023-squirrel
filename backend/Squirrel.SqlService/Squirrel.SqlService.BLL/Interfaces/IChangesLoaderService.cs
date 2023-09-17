using Squirrel.Shared.DTO.DatabaseItem;

namespace Squirrel.SqlService.BLL.Interfaces;

public interface IChangesLoaderService
{
    Task<ICollection<DatabaseItem>> LoadChangesToBlobAsync(Guid changeId, Guid clientId);
}

