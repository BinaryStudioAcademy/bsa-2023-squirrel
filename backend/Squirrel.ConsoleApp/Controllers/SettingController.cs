using Microsoft.AspNetCore.Mvc;
using Squirrel.ConsoleApp.BL.Interfaces;
using Squirrel.ConsoleApp.Models;

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
    public IActionResult Post(DbSettings dbSettings)
    {
        if (!Enum.IsDefined(typeof(DbEngine), dbSettings.DbType))
        {
            throw new NotImplementedException($"Database type {dbSettings.DbType} is not supported.");
        }
        
        _connectionFileService.SaveToFile(dbSettings);

        var clientId = _clientIdFileService.GetClientId();
        return Ok(clientId);
    }
}