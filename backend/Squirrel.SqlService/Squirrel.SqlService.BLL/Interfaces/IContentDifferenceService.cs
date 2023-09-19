using Squirrel.Shared.DTO.DatabaseItem;
using Squirrel.Shared.Enums;
using Squirrel.SqlService.BLL.Models.DTO.Abstract;

namespace Squirrel.SqlService.BLL.Interfaces
{
    public interface IContentDifferenceService
    {
        Task<List<DatabaseItemContentCompare>> GetInlineContentDiffsAsync(int commitId, Guid tempBlobId);
        Task<List<DatabaseItemContentCompare>> GetSideBySideContentDiffsAsync(int commitId, Guid tempBlobId);
        Task GenerateTempBlobContentAsync(int commitId);
    }
}
