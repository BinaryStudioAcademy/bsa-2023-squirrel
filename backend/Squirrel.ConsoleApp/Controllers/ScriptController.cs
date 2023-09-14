using Microsoft.AspNetCore.Mvc;
using Squirrel.ConsoleApp.BL.Interfaces;
using Squirrel.ConsoleApp.Models;
using Squirrel.ConsoleApp.Models.DTO;
using Squirrel.ConsoleApp.Services;

namespace Squirrel.ConsoleApp.Controllers;

[ApiController]
[Route("[controller]")]
public sealed class ScriptController : ControllerBase
{
    private readonly IConnectionFileService _connectionFileService;
    private readonly IConnectionStringService _connectionStringService;

    public ScriptController(IConnectionStringService connectionStringService, IConnectionFileService connectionFileService)
    {
        _connectionStringService = connectionStringService;
        _connectionFileService = connectionFileService;
    }
    
    [HttpPost("execute")]
    public async Task<IActionResult> ExecuteScript([FromBody] ScriptContentDto script)
    {
        var connectionStringDto = _connectionFileService.ReadFromFile();
        var connectionString = _connectionStringService.BuildConnectionString(connectionStringDto);
        var databaseService = DatabaseServiceFactory.CreateDatabaseService(connectionStringDto.DbEngine, connectionString);
        var queryResult = await databaseService.ExecuteQueryAsync(script.Content);
        PrintQueryResult(queryResult);

        return Ok(queryResult);
    }

    // Temporary.
    private void PrintQueryResult(QueryResultTable queryResult)
    {
        Console.WriteLine(queryResult);
        Console.WriteLine("Executed query: ");
        Console.WriteLine(string.Join(" ", queryResult.ColumnNames));
        foreach (var row in queryResult.Rows)
        {
            Console.WriteLine(string.Join(" ", row));
        }
    }
}