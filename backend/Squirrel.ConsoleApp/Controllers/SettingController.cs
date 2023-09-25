using Microsoft.AspNetCore.Mvc;
using Squirrel.ConsoleApp.BL.Interfaces;
using Squirrel.ConsoleApp.Models;

namespace Squirrel.ConsoleApp.Controllers;

[ApiController]
[Route("[controller]")]
public class SettingController : ControllerBase
{
    private readonly IConnectionService _connectionService;

    public SettingController(IConnectionService connectionService)
    {
        _connectionService = connectionService;
    }

    // http://localhost:44567/setting/connect
    [HttpPost("connect")]
    public IActionResult Post(ConnectionStringDto connectionStringDto)
    {
        return Ok(_connectionService.TryConnect(connectionStringDto));
    }
}