using Microsoft.AspNetCore.Mvc;
using Squirrel.ConsoleApp.BL.Interfaces;
using Squirrel.ConsoleApp.Models;

namespace Squirrel.ConsoleApp.Controllers;

[ApiController]
[Route("[controller]")]
public class TablesController: ControllerBase
{
    private readonly IGetActionsService _getActionsService;

    public TablesController(IGetActionsService getActionsService)
    {
        _getActionsService = getActionsService;
    }

    // http://localhost:44567/tables/names
    [HttpGet]
    [Route("names")]
    public async Task<ActionResult<QueryResultTable>> GetTablesNames()
    {
        var names = await _getActionsService.GetAllTablesNamesAsync();

        return Ok(names);
    }

    // http://localhost:44567/tables/structure/dbo/categories
    [HttpGet]
    [Route("structure/{schema}/{name}")]
    public async Task<ActionResult<QueryResultTable>> GetTableStructure([FromRoute] string schema, string name)
    {
        var structure = await _getActionsService.GetTableStructureAsync(schema, name);

        return Ok(structure);
    }

    // http://localhost:44567/tables/constraints/dbo/employees
    [HttpGet]
    [Route("constraints/{schema}/{name}")]
    public async Task<ActionResult<QueryResultTable>> GetTableChecks([FromRoute] string schema, string name)
    {
        var checks = await _getActionsService.GetTableChecksAndUniqueConstraintsAsync(schema, name);

        return Ok(checks);
    }

    // http://localhost:44567/tables/data/dbo/employees/100
    [HttpGet]
    [Route("data/{schema}/{name}/{rowsCount}")]
    public async Task<ActionResult<QueryResultTable>> GetTableData([FromRoute] string schema, string name, int rowsCount)
    {
        var data = await _getActionsService.GetTableDataAsync(schema, name, rowsCount);

        return Ok(data);
    }
}