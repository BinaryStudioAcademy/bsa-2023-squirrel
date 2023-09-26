using Squirrel.Shared.DTO.DatabaseItem;

namespace Squirrel.SqlService.BLL.Interfaces
{
    public interface IContentDifferenceService
    {
        Task<ICollection<DatabaseItemContentCompare>> GetContentDiffsAsync(int commitId, Guid tempBlobId);
        Task<string> GetBlobJson();
        Task CreateCommitContainersWithBlobs();
    }
}
