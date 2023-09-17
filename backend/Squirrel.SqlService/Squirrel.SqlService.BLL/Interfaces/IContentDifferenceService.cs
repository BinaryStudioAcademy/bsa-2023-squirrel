using Squirrel.Shared.DTO.Text;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Squirrel.SqlService.BLL.Interfaces
{
    public interface IContentDifferenceService
    {
        Task<InLineDiffResultDto> GetInlineContentDiffsAsync(int commitId, Guid tempBlobId);
        Task<SideBySideDiffResultDto> GetSideBySideContentDiffsAsync(int commitId, Guid tempBlobId);
    }
}
