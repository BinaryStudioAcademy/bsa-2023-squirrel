using Squirrel.SqlService.BLL.Interfaces.ConsoleAppHub;
using Squirrel.SqlService.BLL.Models.ConsoleAppHub;

namespace Squirrel.SqlService.BLL.Services.ConsoleAppHub;

public class ProcessReceivedDataService : IProcessReceivedDataService
{
    private readonly ResultObserver _resultObserver;

    public ProcessReceivedDataService(ResultObserver resultObserver)
    {
        _resultObserver = resultObserver;
    }
    // TODO: Implement all functions to process data received from ConsoleApp 

    /// <summary>
    /// Just for debugging and demo
    /// </summary>
    private Task ShowResult(Guid httpId, QueryResultTableDTO queryResultTableDTO)
    {
        Console.WriteLine($"------------------------------------------------------------------");
        Console.WriteLine($"Successfully recived data from user '{httpId}'");
        Console.WriteLine($"    result:");
        Console.WriteLine(queryResultTableDTO);
        Console.WriteLine($"------------------------------------------------------------------");
        return Task.CompletedTask;
    }

    public async Task AllTablesNamesProcessReceivedDataAsync(Guid httpId, QueryResultTableDTO queryResultTableDTO)
    {
        _resultObserver.SetResult(httpId, queryResultTableDTO);
        await ShowResult(httpId, queryResultTableDTO);
    }

    public async Task TableDataProcessReceivedDataAsync(Guid httpId, QueryResultTableDTO queryResultTableDTO)
    {
        await ShowResult(httpId, queryResultTableDTO);
    }

    public async Task AllStoredProceduresNamesProcessReceivedDataAsync(Guid httpId, QueryResultTableDTO queryResultTableDTO)
    {
        await ShowResult(httpId, queryResultTableDTO);
    }

    public async Task StoredProcedureDefinitionProcessReceivedDataAsync(Guid httpId, QueryResultTableDTO queryResultTableDTO)
    {
        await ShowResult(httpId, queryResultTableDTO);
    }

    public async Task AllFunctionsNamesProcessReceivedDataAsync(Guid httpId, QueryResultTableDTO queryResultTableDTO)
    {
        await ShowResult(httpId, queryResultTableDTO);
    }

    public async Task FunctionDefinitionProcessReceivedDataAsync(Guid httpId, QueryResultTableDTO queryResultTableDTO)
    {
        await ShowResult(httpId, queryResultTableDTO);
    }

    public async Task AllViewsNamesProcessReceivedDataAsync(Guid httpId, QueryResultTableDTO queryResultTableDTO)
    {
        await ShowResult(httpId, queryResultTableDTO);
    }

    public async Task ViewDefinitionProcessReceivedDataAsync(Guid httpId, QueryResultTableDTO queryResultTableDTO)
    {
        await ShowResult(httpId, queryResultTableDTO);
    }

    public async Task TableStructureProcessReceivedDataAsync(Guid httpId, QueryResultTableDTO queryResultTableDTO)
    {
        await ShowResult(httpId, queryResultTableDTO);
    }

    public async Task TableChecksAndUniqueConstraintsProcessReceivedDataAsync(Guid httpId, QueryResultTableDTO queryResultTableDTO)
    {
        await ShowResult(httpId, queryResultTableDTO);
    }

    public async Task StoredProceduresWithDetailProcessReceivedDataAsync(Guid httpId, QueryResultTableDTO queryResultTableDTO)
    {
        await ShowResult(httpId, queryResultTableDTO);
    }

    public async Task FunctionsWithDetailProcessReceivedDataAsync(Guid httpId, QueryResultTableDTO queryResultTableDTO)
    {
        await ShowResult(httpId, queryResultTableDTO);
    }

    public async Task ViewsWithDetailProcessReceivedDataAsync(Guid httpId, QueryResultTableDTO queryResultTableDTO)
    {
        await ShowResult(httpId, queryResultTableDTO);
    }

    public async Task UserDefinedTypesWithDefaultsAndRulesAndDefinitionProcessReceivedDataAsync(Guid httpId, QueryResultTableDTO queryResultTableDTO)
    {
        await ShowResult(httpId, queryResultTableDTO);
    }

    public async Task UserDefinedTableTypesProcessReceivedDataAsync(Guid httpId, QueryResultTableDTO queryResultTableDTO)
    {
        await ShowResult(httpId, queryResultTableDTO);
    }
}
