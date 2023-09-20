using Squirrel.Shared.DTO.DatabaseItem;
using Squirrel.Shared.Enums;

namespace Squirrel.SqlService.BLL.Interfaces
{
    public interface IContentDifferenceService
    {
        Task<List<DatabaseItemContentCompare>> GetContentDiffsAsync(int commitId, Guid tempBlobId);
        Task GenerateTempBlobContentAsync(int commitId);
    }
}
