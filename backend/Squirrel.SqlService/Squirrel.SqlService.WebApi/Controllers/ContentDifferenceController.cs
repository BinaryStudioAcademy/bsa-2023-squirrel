using Microsoft.AspNetCore.Mvc;
using Squirrel.SqlService.BLL.Interfaces;

namespace Squirrel.SqlService.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public sealed class ContentDifferenceController : ControllerBase
    {
        private readonly IContentDifferenceService _contentDifferenceService;

        public ContentDifferenceController(IContentDifferenceService contentDifference)
        {
            _contentDifferenceService = contentDifference;
        }

        [HttpGet("inline/{commitBlobId}/{tempBlobId}")]
        public async Task<ActionResult> GetInlineContentDifference(string commitBlobId, string tempBlobId)
        {
            return Ok(await _contentDifferenceService.GetInlineContentDiffsAsync(commitBlobId, tempBlobId));
        }

        [HttpGet("sidebyside/{commitBlobId}/{tempBlobId}")]
        public async Task<ActionResult> GetSideBySideContentDifference(string commitBlobId, string tempBlobId)
        {
            return Ok(await _contentDifferenceService.GetSideBySideContentDiffsAsync(commitBlobId, tempBlobId));
        }
    }
}
