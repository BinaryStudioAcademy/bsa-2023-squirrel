using Microsoft.AspNetCore.Mvc;
using Squirrel.ConsoleApp.BL.Interfaces;
using Squirrel.ConsoleApp.Models;
using Squirrel.ConsoleApp.Services;

namespace Squirrel.ConsoleApp.Controllers;

[ApiController]
[Route("[controller]")]
public class SettingController: ControllerBase
{
    private readonly IConnectionFileService _connectionFileService;
    private readonly IGetActionsService _getActionsService;

    public SettingController(IConnectionFileService connectionFileService, IGetActionsService getActionsService)
    {
        _connectionFileService = connectionFileService;
        _getActionsService = getActionsService;
    }
    
    [HttpPost]
    [Route("connect")]
    public IActionResult Post(ConnectionString connectionString)
    {
        _connectionFileService.SaveToFile(connectionString);
        
        //TODO: Connection unique ID
        //TODO: 55 - As a developer I want to setup SignalR connection from console app to webAPI
        var randomId = Guid.NewGuid();
        return Ok(randomId);
    }

    [HttpGet]
    [Route("get-tables-names")]
    public async Task<ActionResult<List<string>>> GetTablesNames()
    {
        var names = await _getActionsService.GetAllTablesNamesAsync();

        return Ok("TESTSTEST");
    }

    [HttpGet]
    [Route("get-table-structure/{tableName}")]
    public async Task<ActionResult<List<string>>> GetTablesStructure(string tableName)
    {
        var names = await _getActionsService.GetDbTablesStructureAsync();

        return Ok(names);
    }
}