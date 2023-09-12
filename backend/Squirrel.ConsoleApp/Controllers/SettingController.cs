using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Squirrel.ConsoleApp.BL.Interfaces;
using Squirrel.ConsoleApp.Models;
using Squirrel.ConsoleApp.Services;

namespace Squirrel.ConsoleApp.Controllers;

[ApiController]
[Route("[controller]")]
public class SettingController : ControllerBase
{
    private readonly IConnectionFileService _connectionFileService;
    private readonly IConnectionStringService _connectionStringService;
    private IDatabaseService? _databaseService;
    private readonly IClientIdFileService _clientIdFileService;
    private readonly IOptionsSnapshot<DbSettings> _dbSettingsOptions;

    public SettingController(IConnectionFileService connectionFileService, IConnectionStringService connectionStringService, IClientIdFileService clientIdFileService, IOptionsSnapshot<DbSettings> dbSettingsOptions)
    {
        _connectionFileService = connectionFileService;
        _connectionStringService = connectionStringService;
        _clientIdFileService = clientIdFileService;
        _dbSettingsOptions = dbSettingsOptions;
    }

    [HttpPost("connect")]
    public IActionResult Post(ConnectionStringDto connectionStringDto)
    {
        _connectionFileService.SaveToFile(connectionStringDto);
        var connectionString = _connectionStringService.BuildConnectionString(connectionStringDto);
        _databaseService = DatabaseServiceFactory.CreateDatabaseService(connectionStringDto.DbEngine, connectionString);
        // Test connection;
        var testQueryResult = _databaseService.ExecuteQuery("SELECT * from Samples");
        Console.WriteLine(testQueryResult);
        
        return Ok(_clientIdFileService.GetClientId());
    }
}