using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Squirrel.ConsoleApp.BL.Interfaces;
using Squirrel.ConsoleApp.Models;

namespace Squirrel.ConsoleApp.Controllers;

[ApiController]
[Route("[controller]")]
public class SettingController: ControllerBase
{
    private readonly IConnectionFileService _connectionFileService;
    private readonly IClientIdFileService _clientIdFileService;
    private readonly IOptionsSnapshot<DbSettings> _dbSettingsOptions;

    public SettingController(IConnectionFileService connectionFileService, IClientIdFileService clientIdFileService, IOptionsSnapshot<DbSettings> dbSettingsOptions)
    {
        _connectionFileService = connectionFileService;
        _clientIdFileService = clientIdFileService;
        _dbSettingsOptions = dbSettingsOptions;
    }

    [HttpPost("connect")]
    public IActionResult Post(DbSettings dbSettings)
    {
        _connectionFileService.SaveToFile(dbSettings);

        return Ok(_clientIdFileService.GetClientId());
    }
}