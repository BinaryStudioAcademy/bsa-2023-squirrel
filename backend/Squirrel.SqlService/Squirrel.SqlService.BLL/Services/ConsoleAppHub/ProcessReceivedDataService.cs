using AutoMapper;
using Squirrel.SqlService.BLL.Interfaces.ConsoleAppHub;
using Squirrel.SqlService.BLL.Models.ConsoleAppHub;
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
    private Task ShowResult(string clientId, QueryResultTableDTO queryResultTableDTO)
    {
        Console.WriteLine($"------------------------------------------------------------------");
        Console.WriteLine($"Successfully recived data from user '{clientId}'");
        Console.WriteLine($"    result:");
        Console.WriteLine(queryResultTableDTO);
        Console.WriteLine($"------------------------------------------------------------------");
        return Task.CompletedTask;
    }

    public async Task<TableNamesDto> AllTablesNamesProcessReceivedDataAsync(string clientId, QueryResultTableDTO queryResultTableDTO)
    {
        await ShowResult(clientId, queryResultTableDTO);
        return _mapper.Map<TableNamesDto>(queryResultTableDTO);
    }

    public async Task TableDataProcessReceivedDataAsync(string clientId, QueryResultTableDTO queryResultTableDTO)
    {
        await ShowResult(clientId, queryResultTableDTO);
    }

    public async Task AllStoredProceduresNamesProcessReceivedDataAsync(string clientId, QueryResultTableDTO queryResultTableDTO)
    {
        await ShowResult(clientId, queryResultTableDTO);
    }

    public async Task StoredProcedureDefinitionProcessReceivedDataAsync(string clientId, QueryResultTableDTO queryResultTableDTO)
    {
        await ShowResult(clientId, queryResultTableDTO);
    }

    public async Task AllFunctionsNamesProcessReceivedDataAsync(string clientId, QueryResultTableDTO queryResultTableDTO)
    {
        await ShowResult(clientId, queryResultTableDTO);
    }

    public async Task FunctionDefinitionProcessReceivedDataAsync(string clientId, QueryResultTableDTO queryResultTableDTO)
    {
        await ShowResult(clientId, queryResultTableDTO);
    }

    public async Task AllViewsNamesProcessReceivedDataAsync(string clientId, QueryResultTableDTO queryResultTableDTO)
    {
        await ShowResult(clientId, queryResultTableDTO);
    }

    public async Task ViewDefinitionProcessReceivedDataAsync(string clientId, QueryResultTableDTO queryResultTableDTO)
    {
        await ShowResult(clientId, queryResultTableDTO);
    }

    public async Task TableStructureProcessReceivedDataAsync(string clientId, QueryResultTableDTO queryResultTableDTO)
    {
        await ShowResult(clientId, queryResultTableDTO);
    }

    public async Task TableChecksAndUniqueConstraintsProcessReceivedDataAsync(string clientId, QueryResultTableDTO queryResultTableDTO)
    {
        await ShowResult(clientId, queryResultTableDTO);
    }

    public async Task StoredProceduresWithDetailProcessReceivedDataAsync(string clientId, QueryResultTableDTO queryResultTableDTO)
    {
        await ShowResult(clientId, queryResultTableDTO);
    }

    public async Task FunctionsWithDetailProcessReceivedDataAsync(string clientId, QueryResultTableDTO queryResultTableDTO)
    {
        await ShowResult(clientId, queryResultTableDTO);
    }

    public async Task ViewsWithDetailProcessReceivedDataAsync(string clientId, QueryResultTableDTO queryResultTableDTO)
    {
        await ShowResult(clientId, queryResultTableDTO);
    }

    public async Task UserDefinedTypesWithDefaultsAndRulesAndDefinitionProcessReceivedDataAsync(string clientId, QueryResultTableDTO queryResultTableDTO)
    {
        await ShowResult(clientId, queryResultTableDTO);
    }

    public async Task UserDefinedTableTypesProcessReceivedDataAsync(string clientId, QueryResultTableDTO queryResultTableDTO)
    {
        await ShowResult(clientId, queryResultTableDTO);
    }
}
