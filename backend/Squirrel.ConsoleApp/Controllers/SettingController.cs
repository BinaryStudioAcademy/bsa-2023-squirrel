using Microsoft.AspNetCore.Mvc;
using Squirrel.ConsoleApp.BL.Interfaces;
using Squirrel.ConsoleApp.Models;

namespace Squirrel.ConsoleApp.Controllers;

[ApiController]
[Route("[controller]")]
public class SettingController: ControllerBase
{
    private readonly IConnectionFileService _connectionFileService;

    public SettingController(IConnectionFileService connectionFileService)
    {
        _connectionFileService = connectionFileService;
    }
    
    [HttpPost]
    [Route("connect")]
    public IActionResult Post(DbSettings dbSettings)
    {
        _connectionFileService.SaveToFile(dbSettings);
        
        //TODO: Connection unique ID
        //TODO: 55 - As a developer I want to setup SignalR connection from console app to webAPI
        var randomId = Guid.NewGuid();
        return Ok(randomId);
    }
}