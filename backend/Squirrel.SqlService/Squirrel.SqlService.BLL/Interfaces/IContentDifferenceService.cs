using Squirrel.Shared.DTO.DatabaseItem;

namespace Squirrel.SqlService.BLL.Interfaces
{
    public interface IContentDifferenceService
    {
        Task<List<DatabaseItemContentCompare>> GetContentDiffsAsync(int commitId, Guid tempBlobId);
    }
}
