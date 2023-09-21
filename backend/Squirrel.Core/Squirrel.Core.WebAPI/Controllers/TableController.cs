using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Squirrel.Core.BLL.Interfaces;
using Squirrel.Shared.DTO.ConsoleAppHub;
using Squirrel.Shared.DTO.Table;
using Squirrel.SqlService.BLL.Models.DTO;

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
    public async Task<ActionResult<TableNamesDto>> GetTableNames(QueryParameters queryParameters)
    {
        return Ok(await _tableService.GetTablesName(queryParameters));
    }
}