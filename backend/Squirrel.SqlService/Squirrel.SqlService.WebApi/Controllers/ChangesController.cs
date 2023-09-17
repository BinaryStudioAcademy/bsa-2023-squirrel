using Microsoft.AspNetCore.Mvc;
using Squirrel.Shared.DTO.DatabaseItem;
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
    public async Task<ActionResult<ICollection<DatabaseItem>>> SaveChangesToBlob([FromBody] Guid changeId, Guid clientId)
    {
        return Ok(await _changesLoaderService.LoadChangesToBlobAsync(changeId, clientId));
    }
}
