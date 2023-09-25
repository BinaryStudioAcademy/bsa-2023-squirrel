using Microsoft.AspNetCore.Mvc;
using Squirrel.SqlService.BLL.Interfaces;

namespace Squirrel.SqlService.WebApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public sealed class ChangesController : ControllerBase
{
    private readonly IChangesLoaderService _changesLoaderService;

    public ChangesController(IChangesLoaderService changesLoaderService)
    {
        _changesLoaderService = changesLoaderService;
    }

    [HttpPost("{clientId}")]
    public async Task<ActionResult> SaveChangesToBlobAsync([FromBody] Guid changeId, Guid clientId)
    {
        await _changesLoaderService.LoadChangesToBlobAsync(changeId, clientId);

        return Ok();
    }
}
