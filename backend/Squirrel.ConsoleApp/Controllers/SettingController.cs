using Microsoft.AspNetCore.Mvc;
using Squirrel.ConsoleApp.BL.Interfaces;
using Squirrel.ConsoleApp.Models;

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
}