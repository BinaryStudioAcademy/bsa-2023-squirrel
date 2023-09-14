using Microsoft.AspNetCore.Mvc;
using Squirrel.ConsoleApp.BL.Exceptions;
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
    private readonly IClientIdFileService _clientIdFileService;

    public SettingController(IConnectionFileService connectionFileService, IConnectionStringService connectionStringService, IClientIdFileService clientIdFileService)
    {
        _connectionFileService = connectionFileService;
        _connectionStringService = connectionStringService;
        _clientIdFileService = clientIdFileService;
    }

    // http://localhost:44567/setting/connect
    [HttpPost("connect")]
    public IActionResult Post(ConnectionStringDto connectionStringDto)
    {
        connectionStringDto.IntegratedSecurity = true;
        var connectionString = _connectionStringService.BuildConnectionString(connectionStringDto);
        var databaseService = DatabaseServiceFactory.CreateDatabaseService(connectionStringDto.DbEngine, connectionString);
        var databaseProvider = DatabaseServiceFactory.CreateDbQueryProvider(connectionStringDto.DbEngine);

        // Test connection;
        try
        {
            var testQueryResult = databaseService.ExecuteQuery(databaseProvider.GetTablesNamesQuery());
            Console.WriteLine(testQueryResult);
        }
        catch (Exception ex)
        {
            throw new DbConnectionFailed(connectionString, ex.Message);
        }

        _connectionFileService.SaveToFile(connectionStringDto);

        var clientId = _clientIdFileService.GetClientId();

        return Ok(clientId);
    }
}