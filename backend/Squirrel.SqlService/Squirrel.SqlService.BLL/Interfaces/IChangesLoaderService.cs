namespace Squirrel.SqlService.BLL.Interfaces;

public interface IChangesLoaderService
{
    Task LoadChangesToBlobAsync(Guid changeId, Guid clientId);
}

