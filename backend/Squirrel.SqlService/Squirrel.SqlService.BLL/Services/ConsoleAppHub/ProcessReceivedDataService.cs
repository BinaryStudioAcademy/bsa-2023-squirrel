using AutoMapper;
using Squirrel.ConsoleApp.Models;
using Squirrel.SqlService.BLL.Interfaces.ConsoleAppHub;
using Squirrel.SqlService.BLL.Models.DTO;

namespace Squirrel.SqlService.BLL.Services.ConsoleAppHub;

public class ProcessReceivedDataService : IProcessReceivedDataService
{
    private readonly IMapper _mapper;
    public ProcessReceivedDataService(IMapper mapper)
    {
        _mapper = mapper;
    }

    // TODO: Implement all functions to process data received from ConsoleApp 

    /// <summary>
    /// Just for debugging and demo
    /// </summary>
    private Task ShowResult(string clientId, QueryResultTable queryResultTable)
    {
        Console.WriteLine($"------------------------------------------------------------------");
        Console.WriteLine($"Successfully recived data from user '{clientId}'");
        Console.WriteLine($"    result:");
        Console.WriteLine(queryResultTable);
        Console.WriteLine($"------------------------------------------------------------------");
        return Task.CompletedTask;
    }

    public async Task<TableNamesDto> AllTablesNamesProcessReceivedDataAsync(string clientId, QueryResultTable queryResultTable)
    {
        await ShowResult(clientId, queryResultTable);
        return _mapper.Map<TableNamesDto>(queryResultTable);
    }

    public async Task<TableDataDto> TableDataProcessReceivedDataAsync(string clientId, QueryResultTable queryResultTable)
    {
        await ShowResult(clientId, queryResultTable);
        return _mapper.Map<TableDataDto>(queryResultTable);
    }

    public async Task<TableStructureDto> TableStructureProcessReceivedDataAsync(string clientId, QueryResultTable queryResultTable)
    {
        await ShowResult(clientId, queryResultTable);
        return _mapper.Map<TableStructureDto>(queryResultTable);
    }

    public async Task<TableConstraintsDto> TableChecksAndUniqueConstraintsProcessReceivedDataAsync(string clientId, QueryResultTable queryResultTable)
    {
        await ShowResult(clientId, queryResultTable);
        return _mapper.Map<TableConstraintsDto>(queryResultTable);
    }

    public async Task AllStoredProceduresNamesProcessReceivedDataAsync(string clientId, QueryResultTable queryResultTable)
    {
        await ShowResult(clientId, queryResultTable);
    }

    public async Task StoredProcedureDefinitionProcessReceivedDataAsync(string clientId, QueryResultTable queryResultTable)
    {
        await ShowResult(clientId, queryResultTable);
    }

    public async Task AllFunctionsNamesProcessReceivedDataAsync(string clientId, QueryResultTable queryResultTable)
    {
        await ShowResult(clientId, queryResultTable);
    }

    public async Task FunctionDefinitionProcessReceivedDataAsync(string clientId, QueryResultTable queryResultTable)
    {
        await ShowResult(clientId, queryResultTable);
    }

    public async Task AllViewsNamesProcessReceivedDataAsync(string clientId, QueryResultTable queryResultTable)
    {
        await ShowResult(clientId, queryResultTable);
    }

    public async Task ViewDefinitionProcessReceivedDataAsync(string clientId, QueryResultTable queryResultTable)
    {
        await ShowResult(clientId, queryResultTable);
    }

    public async Task StoredProceduresWithDetailProcessReceivedDataAsync(string clientId, QueryResultTable queryResultTable)
    {
        await ShowResult(clientId, queryResultTable);
    }

    public async Task FunctionsWithDetailProcessReceivedDataAsync(string clientId, QueryResultTable queryResultTable)
    {
        await ShowResult(clientId, queryResultTable);
    }

    public async Task ViewsWithDetailProcessReceivedDataAsync(string clientId, QueryResultTable queryResultTable)
    {
        await ShowResult(clientId, queryResultTable);
    }

    public async Task UserDefinedTypesWithDefaultsAndRulesAndDefinitionProcessReceivedDataAsync(string clientId, QueryResultTable queryResultTable)
    {
        await ShowResult(clientId, queryResultTable);
    }

    public async Task UserDefinedTableTypesProcessReceivedDataAsync(string clientId, QueryResultTable queryResultTable)
    {
        await ShowResult(clientId, queryResultTable);
    }
}
