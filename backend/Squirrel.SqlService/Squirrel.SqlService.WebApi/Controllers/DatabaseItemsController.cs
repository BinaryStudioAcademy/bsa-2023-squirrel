using Microsoft.AspNetCore.Mvc;
using Squirrel.Shared.DTO.DatabaseItem;
using Squirrel.SqlService.BLL.Interfaces;

namespace Squirrel.SqlService.WebApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public sealed class DatabaseItemsController : ControllerBase
{
    private readonly IDbItemsRetrievalService _dbItemsRetrieval;

    public DatabaseItemsController(IDbItemsRetrievalService dbItemsRetrieval)
    {
        _dbItemsRetrieval = dbItemsRetrieval;
    }

    [HttpGet("{clientId}")]
    public async Task<ActionResult<List<DatabaseItem>>> GetAllItemsAsync(Guid clientId)
    {
        return Ok(await _dbItemsRetrieval.GetAllItemsAsync(clientId));
    }
}
