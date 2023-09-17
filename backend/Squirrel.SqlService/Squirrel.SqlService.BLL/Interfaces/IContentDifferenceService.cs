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
        Task<InLineDiffResultDto> GetInlineContentDiffsAsync(string commitBlobId, string tempBlobId);
        Task<SideBySideDiffResultDto> GetSideBySideContentDiffsAsync(string commitBlobId, string tempBlobId);
    }
}
