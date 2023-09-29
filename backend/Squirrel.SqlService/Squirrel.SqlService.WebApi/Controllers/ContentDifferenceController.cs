using Microsoft.AspNetCore.Mvc;
using Squirrel.Shared.DTO.DatabaseItem;
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
    public async Task<ActionResult<ICollection<DatabaseItemContentCompare>>> GetContentDiffsAsync(int commitId, Guid tempBlobId)
    {
        return Ok(await _contentDifferenceService.GetContentDiffsAsync(commitId, tempBlobId));
    }
    
    [HttpGet("getBlobContent/{blobId}")]
    public async Task<ActionResult<string>> GetBlob(Guid blobId)
    {
        return Ok(await _contentDifferenceService.GetContentAsync(blobId));
    }
}
