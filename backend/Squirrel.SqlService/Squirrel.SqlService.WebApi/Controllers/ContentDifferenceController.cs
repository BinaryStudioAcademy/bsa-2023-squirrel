using Microsoft.AspNetCore.Mvc;
using Squirrel.SqlService.BLL.Interfaces;

namespace Squirrel.SqlService.WebApi.Controllers;

[Route("api/[controller]")]
[ApiController]
public sealed class ContentDifferenceController : ControllerBase
{
    private readonly IContentDifferenceService _contentDifferenceService;

    public ContentDifferenceController(IContentDifferenceService contentDifference)
    {
        _contentDifferenceService = contentDifference;
    }

    [HttpGet("inline/{commitId}/{tempBlobId}")]
    public async Task<ActionResult> GetInlineContentDifference(int commitId, Guid tempBlobId)
    {
        return Ok(await _contentDifferenceService.GetInlineContentDiffsAsync(commitId, tempBlobId));
    }

    [HttpGet("sidebyside/{commitId}/{tempBlobId}")]
    public async Task<ActionResult> GetSideBySideContentDifference(int commitId, Guid tempBlobId)
    {
        return Ok(await _contentDifferenceService.GetSideBySideContentDiffsAsync(commitId, tempBlobId));
    }
}
