using Microsoft.AspNetCore.Mvc;
using Squirrel.Core.BLL.Interfaces;
using Squirrel.Shared.DTO.DatabaseItem;

namespace Squirrel.Core.WebAPI.Controllers;

[ApiController]
[Route("api/[controller]")]
public sealed class DatabaseItemsController : ControllerBase
{
    private readonly IDatabaseItemsService _databaseItemsService;

    public DatabaseItemsController(IDatabaseItemsService databaseItemsService)
    {
        _databaseItemsService = databaseItemsService;
    }

    [HttpGet("{clientId}")]
    public async Task<ActionResult<List<DatabaseItem>>> GetAllItems(Guid clientId)
    {
        return Ok(await _databaseItemsService.GetAllItemsAsync(clientId));
    }
}

