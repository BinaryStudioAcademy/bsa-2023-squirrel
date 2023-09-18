using AutoMapper;
using Squirrel.ConsoleApp.Models;
using Squirrel.SqlService.BLL.Interfaces.ConsoleAppHub;
using Squirrel.SqlService.BLL.Models.DTO.Shared;

namespace Squirrel.SqlService.BLL.Services.ConsoleAppHub;

public class ProcessReceivedDataService : IProcessReceivedDataService
{
    private readonly ResultObserver _resultObserver;
    private readonly IMapper _mapper;
    public ProcessReceivedDataService(IMapper mapper, ResultObserver resultObserver)
    {
        _mapper = mapper;
        _resultObserver = resultObserver;
    }

    private Task ShowResult(Guid queryId, QueryResultTable queryResultTable)
    {
        Console.WriteLine($"------------------------------------------------------------------");
        Console.WriteLine($"Successfully recived data from user '{queryId}'");
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

    public async Task<RoutineDefinitionDto> FunctionDefinitionProcessReceivedDataAsync(Guid queryId, QueryResultTable queryResultTable)
    {
        await ShowResult(queryId, queryResultTable);
        return _mapper.Map<RoutineDefinitionDto>(queryResultTable);
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

    public async Task<RoutineDefinitionDto> StoredProceduresWithDetailProcessReceivedDataAsync(Guid queryId, QueryResultTable queryResultTable)
    {
        await ShowResult(queryId, queryResultTable);
        return _mapper.Map<RoutineDefinitionDto>(queryResultTable);
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
}
