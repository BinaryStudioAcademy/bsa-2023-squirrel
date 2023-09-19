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

    [HttpGet("{commitId}/{tempBlobId}")]
    public async Task<ActionResult> GetContentDiffsAsync(int commitId, Guid tempBlobId)
    {
        return Ok(await _contentDifferenceService.GetContentDiffsAsync(commitId, tempBlobId));
    }
}
