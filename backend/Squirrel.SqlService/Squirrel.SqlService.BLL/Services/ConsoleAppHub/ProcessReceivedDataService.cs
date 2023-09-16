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
    private Task ShowResult(Guid httpId, QueryResultTableDTO queryResultTableDTO)
    {
        Console.WriteLine($"------------------------------------------------------------------");
        Console.WriteLine($"Successfully recived data from user '{httpId}'");
        Console.WriteLine($"    result:");
        Console.WriteLine(queryResultTableDTO);
        Console.WriteLine($"------------------------------------------------------------------");
        return Task.CompletedTask;
    }

    public async Task AllTablesNamesProcessReceivedDataAsync(Guid queryId, QueryResultTableDTO queryResultTableDTO)
    {
        _resultObserver.SetResult(queryId, queryResultTableDTO);
        await ShowResult(queryId, queryResultTableDTO);
    }

    public async Task TableDataProcessReceivedDataAsync(Guid queryId, QueryResultTableDTO queryResultTableDTO)
    {
        _resultObserver.SetResult(queryId, queryResultTableDTO);
        await ShowResult(queryId, queryResultTableDTO);
    }

    public async Task AllStoredProceduresNamesProcessReceivedDataAsync(Guid queryId, QueryResultTableDTO queryResultTableDTO)
    {
        _resultObserver.SetResult(queryId, queryResultTableDTO);
        await ShowResult(queryId, queryResultTableDTO);
    }

    public async Task StoredProcedureDefinitionProcessReceivedDataAsync(Guid queryId, QueryResultTableDTO queryResultTableDTO)
    {
        _resultObserver.SetResult(queryId, queryResultTableDTO);
        await ShowResult(queryId, queryResultTableDTO);
    }

    public async Task AllFunctionsNamesProcessReceivedDataAsync(Guid queryId, QueryResultTableDTO queryResultTableDTO)
    {
        _resultObserver.SetResult(queryId, queryResultTableDTO);
        await ShowResult(queryId, queryResultTableDTO);
    }

    public async Task FunctionDefinitionProcessReceivedDataAsync(Guid queryId, QueryResultTableDTO queryResultTableDTO)
    {
        _resultObserver.SetResult(queryId, queryResultTableDTO);
        await ShowResult(queryId, queryResultTableDTO);
    }

    public async Task AllViewsNamesProcessReceivedDataAsync(Guid queryId, QueryResultTableDTO queryResultTableDTO)
    {
        _resultObserver.SetResult(queryId, queryResultTableDTO);
        await ShowResult(queryId, queryResultTableDTO);
    }

    public async Task ViewDefinitionProcessReceivedDataAsync(Guid queryId, QueryResultTableDTO queryResultTableDTO)
    {
        _resultObserver.SetResult(queryId, queryResultTableDTO);
        await ShowResult(queryId, queryResultTableDTO);
    }

    public async Task TableStructureProcessReceivedDataAsync(Guid queryId, QueryResultTableDTO queryResultTableDTO)
    {
        _resultObserver.SetResult(queryId, queryResultTableDTO);
        await ShowResult(queryId, queryResultTableDTO);
    }

    public async Task TableChecksAndUniqueConstraintsProcessReceivedDataAsync(Guid queryId, QueryResultTableDTO queryResultTableDTO)
    {
        _resultObserver.SetResult(queryId, queryResultTableDTO);
        await ShowResult(queryId, queryResultTableDTO);
    }

    public async Task StoredProceduresWithDetailProcessReceivedDataAsync(Guid queryId, QueryResultTableDTO queryResultTableDTO)
    {
        _resultObserver.SetResult(queryId, queryResultTableDTO);
        await ShowResult(queryId, queryResultTableDTO);
    }

    public async Task FunctionsWithDetailProcessReceivedDataAsync(Guid queryId, QueryResultTableDTO queryResultTableDTO)
    {
        _resultObserver.SetResult(queryId, queryResultTableDTO);
        await ShowResult(queryId, queryResultTableDTO);
    }

    public async Task ViewsWithDetailProcessReceivedDataAsync(Guid queryId, QueryResultTableDTO queryResultTableDTO)
    {
        _resultObserver.SetResult(queryId, queryResultTableDTO);
        await ShowResult(queryId, queryResultTableDTO);
    }

    public async Task UserDefinedTypesWithDefaultsAndRulesAndDefinitionProcessReceivedDataAsync(Guid queryId, QueryResultTableDTO queryResultTableDTO)
    {
        _resultObserver.SetResult(queryId, queryResultTableDTO);
        await ShowResult(queryId, queryResultTableDTO);
    }

    public async Task UserDefinedTableTypesProcessReceivedDataAsync(Guid queryId, QueryResultTableDTO queryResultTableDTO)
    {
        _resultObserver.SetResult(queryId, queryResultTableDTO);
        await ShowResult(queryId, queryResultTableDTO);
    }
}
