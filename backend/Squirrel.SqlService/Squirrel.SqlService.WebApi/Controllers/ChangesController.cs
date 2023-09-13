using Microsoft.AspNetCore.Mvc;
using Squirrel.AzureBlobStorage.Models;
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

    [HttpPost]
    public async Task<ActionResult<Blob>> SaveChangesToBlob([FromBody] Guid changeId)
    {
        return Ok(await _changesLoaderService.LoadChangesToBlobAsync(changeId));
    }
}
