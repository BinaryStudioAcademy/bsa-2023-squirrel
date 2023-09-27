using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Squirrel.Core.BLL.Interfaces;
using Squirrel.Shared.DTO.ConsoleAppHub;
using Squirrel.Shared.DTO.Table;

namespace Squirrel.Core.WebAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
[Authorize]
public class TableController : ControllerBase
{
    private readonly ITableService _tableService;

    public TableController(ITableService tableService)
    {
        _tableService = tableService;
    }

    [HttpPost]
    public async Task<ActionResult<TableNamesDto>> GetTableNamesAsync(QueryParameters queryParameters)
    {
        return Ok(await _tableService.GetTablesNameAsync(queryParameters));
    }

    [HttpPost("table-structure")]
    public async Task<ActionResult<TableNamesDto>> GetTableStructureAsync(QueryParameters queryParameters)
    {
        return Ok(await _tableService.GetTableStructureAsync(queryParameters));
    }
}