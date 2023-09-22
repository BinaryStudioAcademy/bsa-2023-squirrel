using Squirrel.ConsoleApp.Models;
using Squirrel.SqlService.BLL.Interfaces.ConsoleAppHub;

namespace Squirrel.SqlService.BLL.Services.ConsoleAppHub;

public class ProcessReceivedDataService : IProcessReceivedDataService
{
    private readonly ResultObserver _resultObserver;
    public ProcessReceivedDataService(ResultObserver resultObserver)
    {
        _resultObserver = resultObserver;
    }

    private Task ShowResult(Guid queryId, QueryResultTable queryResultTable)
    {
        Console.WriteLine($"------------------------------------------------------------------");
        Console.WriteLine($"Successfully received data from user '{queryId}'");
        Console.WriteLine($"    result:");
        Console.WriteLine(queryResultTable);
        Console.WriteLine($"------------------------------------------------------------------");
        return Task.CompletedTask;
    }
    
    public async Task AllTablesNamesProcessReceivedDataAsync(Guid queryId, QueryResultTable queryResultTable)
    {
        _resultObserver.SetResult(queryId, queryResultTable);
        await ShowResult(queryId, queryResultTable);
    }

    public async Task TableDataProcessReceivedDataAsync(Guid queryId, QueryResultTable queryResultTable)
    {
        _resultObserver.SetResult(queryId, queryResultTable);
        await ShowResult(queryId, queryResultTable);
    }

    public async Task AllStoredProceduresNamesProcessReceivedDataAsync(Guid queryId, QueryResultTable queryResultTable)
    {
        _resultObserver.SetResult(queryId, queryResultTable);
        await ShowResult(queryId, queryResultTable);
    }

    public async Task AllFunctionsNamesProcessReceivedDataAsync(Guid queryId, QueryResultTable queryResultTable)
    {
        _resultObserver.SetResult(queryId, queryResultTable);
        await ShowResult(queryId, queryResultTable);
    }
    
    public async Task FunctionDefinitionProcessReceivedDataAsync(Guid queryId, QueryResultTable queryResultTable)
    {
        _resultObserver.SetResult(queryId, queryResultTable);
        await ShowResult(queryId, queryResultTable);
    }

    public async Task AllViewsNamesProcessReceivedDataAsync(Guid queryId, QueryResultTable queryResultTable)
    {
        _resultObserver.SetResult(queryId, queryResultTable);
        await ShowResult(queryId, queryResultTable);
    }

    public async Task ViewDefinitionProcessReceivedDataAsync(Guid queryId, QueryResultTable queryResultTable)
    {
        _resultObserver.SetResult(queryId, queryResultTable);
        await ShowResult(queryId, queryResultTable);
    }

    public async Task TableStructureProcessReceivedDataAsync(Guid queryId, QueryResultTable queryResultTable)
    {
        _resultObserver.SetResult(queryId, queryResultTable);
        await ShowResult(queryId, queryResultTable);
    }

    public async Task TableChecksAndUniqueConstraintsProcessReceivedDataAsync(Guid queryId, QueryResultTable queryResultTable)
    {
        _resultObserver.SetResult(queryId, queryResultTable);
        await ShowResult(queryId, queryResultTable);
    }

    public async Task StoredProceduresWithDetailProcessReceivedDataAsync(Guid queryId, QueryResultTable queryResultTable)
    {
        _resultObserver.SetResult(queryId, queryResultTable);
        await ShowResult(queryId, queryResultTable);
    }

    public async Task FunctionsWithDetailProcessReceivedDataAsync(Guid queryId, QueryResultTable queryResultTable)
    {
        _resultObserver.SetResult(queryId, queryResultTable);
        await ShowResult(queryId, queryResultTable);
    }

    public async Task ViewsWithDetailProcessReceivedDataAsync(Guid queryId, QueryResultTable queryResultTable)
    {
        _resultObserver.SetResult(queryId, queryResultTable);
        await ShowResult(queryId, queryResultTable);
    }

    public async Task UserDefinedTypesWithDefaultsAndRulesAndDefinitionProcessReceivedDataAsync(Guid queryId, QueryResultTable queryResultTable)
    {
        _resultObserver.SetResult(queryId, queryResultTable);
        await ShowResult(queryId, queryResultTable);
    }

    public async Task UserDefinedTableTypesProcessReceivedDataAsync(Guid queryId, QueryResultTable queryResultTable)
    {
        _resultObserver.SetResult(queryId, queryResultTable);
        await ShowResult(queryId, queryResultTable);
    }

    public async Task ExecuteScriptProcessReceivedDataAsync(Guid queryId, QueryResultTable queryResultTable)
    {
        _resultObserver.SetResult(queryId, queryResultTable);
        await ShowResult(queryId, queryResultTable);
    }
}
