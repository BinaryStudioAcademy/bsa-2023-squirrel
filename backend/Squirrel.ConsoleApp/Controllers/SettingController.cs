using Microsoft.AspNetCore.Mvc;
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

    public SettingController(IConnectionFileService connectionFileService, IConnectionStringService connectionStringService)
    {
        _connectionFileService = connectionFileService;
        _connectionStringService = connectionStringService;
    }
    
    [HttpPost]
    [Route("connect")]
    public IActionResult Connect(ConnectionStringDto connectionStringDto)
    {
        _connectionFileService.SaveToFile(connectionStringDto);
        var connectionString = _connectionStringService.BuildConnectionString(connectionStringDto);
        _databaseService = DatabaseServiceFactory.CreateDatabaseService(connectionStringDto.DbEngine, connectionString);
        // Test connection;
        var testQueryResult = _databaseService.ExecuteQuery("SELECT * from Users");
        Console.WriteLine(testQueryResult);
        
        //TODO: Connection unique ID
        //TODO: 55 - As a developer I want to setup SignalR connection from console app to webAPI
        var randomId = Guid.NewGuid();
        return Ok(randomId);
    }
}