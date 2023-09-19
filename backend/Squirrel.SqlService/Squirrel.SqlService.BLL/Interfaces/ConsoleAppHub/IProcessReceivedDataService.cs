using Squirrel.ConsoleApp.Models;

namespace Squirrel.SqlService.BLL.Interfaces.ConsoleAppHub;

public interface IProcessReceivedDataService
{
    // Actions
    Task AllTablesNamesProcessReceivedDataAsync(Guid queryId, QueryResultTable queryResultTable);
    Task TableDataProcessReceivedDataAsync(Guid queryId, QueryResultTable queryResultTable);

    Task AllStoredProceduresNamesProcessReceivedDataAsync(Guid queryId, QueryResultTable queryResultTable);

    Task AllFunctionsNamesProcessReceivedDataAsync(Guid queryId, QueryResultTable queryResultTable);
    Task FunctionDefinitionProcessReceivedDataAsync(Guid queryId, QueryResultTable queryResultTable);
    
    Task AllViewsNamesProcessReceivedDataAsync(Guid queryId, QueryResultTable queryResultTable);
    Task ViewDefinitionProcessReceivedDataAsync(Guid queryId, QueryResultTable queryResultTable);

    Task TableStructureProcessReceivedDataAsync(Guid queryId, QueryResultTable queryResultTable);
    Task TableChecksAndUniqueConstraintsProcessReceivedDataAsync(Guid queryId, QueryResultTable queryResultTable);

    Task StoredProceduresWithDetailProcessReceivedDataAsync(Guid queryId, QueryResultTable queryResultTable);
    Task FunctionsWithDetailProcessReceivedDataAsync(Guid queryId, QueryResultTable queryResultTable);
    Task ViewsWithDetailProcessReceivedDataAsync(Guid queryId, QueryResultTable queryResultTable);

    Task UserDefinedTypesWithDefaultsAndRulesAndDefinitionProcessReceivedDataAsync(Guid queryId, QueryResultTable queryResultTable);
    Task UserDefinedTableTypesProcessReceivedDataAsync(Guid queryId, QueryResultTable queryResultTable);

    Task RemoteConnectProcessAsync(Guid queryId, QueryResultTable queryResultTable);

}