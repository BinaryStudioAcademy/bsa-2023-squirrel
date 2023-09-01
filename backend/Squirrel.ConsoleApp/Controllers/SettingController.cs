using Microsoft.AspNetCore.Mvc;
using Squirrel.ConsoleApp.BL.Interfaces;
using Squirrel.ConsoleApp.Models.Models;

namespace Squirrel.ConsoleApp.Controllers;

[ApiController]
[Route("[controller]")]
public class SettingController: ControllerBase
{
    private readonly IConnectionFileService _connectionFileService;
    private readonly IClientIdFileService _clientIdFileService;

    public SettingController(IConnectionFileService connectionFileService, IClientIdFileService clientIdFileService)
    {
        _connectionFileService = connectionFileService;
        _clientIdFileService = clientIdFileService;
    }
    
    [HttpPost]
    [Route("connect")]
    public IActionResult Post(ConnectionString connectionString)
    {
        _connectionFileService.SaveToFile(connectionString);
        
        var clientId = _clientIdFileService.GetClientId();
        return Ok(clientId);
    }
}