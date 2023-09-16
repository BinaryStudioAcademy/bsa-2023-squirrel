using Squirrel.SqlService.BLL.Models.ConsoleAppHub;

namespace Squirrel.SqlService.BLL.Interfaces.ConsoleAppHub;

public interface IProcessReceivedDataService
{
    // Actions
    Task AllTablesNamesProcessReceivedDataAsync(Guid queryId, QueryResultTableDTO queryResultTableDTO);
    Task TableDataProcessReceivedDataAsync(Guid queryId, QueryResultTableDTO queryResultTableDTO);

    Task AllStoredProceduresNamesProcessReceivedDataAsync(Guid queryId, QueryResultTableDTO queryResultTableDTO);
    Task StoredProcedureDefinitionProcessReceivedDataAsync(Guid queryId, QueryResultTableDTO queryResultTableDTO);

    Task AllFunctionsNamesProcessReceivedDataAsync(Guid queryId, QueryResultTableDTO queryResultTableDTO);
    Task FunctionDefinitionProcessReceivedDataAsync(Guid queryId, QueryResultTableDTO queryResultTableDTO);

    Task AllViewsNamesProcessReceivedDataAsync(Guid queryId, QueryResultTableDTO queryResultTableDTO);
    Task ViewDefinitionProcessReceivedDataAsync(Guid queryId, QueryResultTableDTO queryResultTableDTO);

    Task TableStructureProcessReceivedDataAsync(Guid queryId, QueryResultTableDTO queryResultTableDTO);
    Task TableChecksAndUniqueConstraintsProcessReceivedDataAsync(Guid queryId, QueryResultTableDTO queryResultTableDTO);

    Task StoredProceduresWithDetailProcessReceivedDataAsync(Guid queryId, QueryResultTableDTO queryResultTableDTO);
    Task FunctionsWithDetailProcessReceivedDataAsync(Guid queryId, QueryResultTableDTO queryResultTableDTO);
    Task ViewsWithDetailProcessReceivedDataAsync(Guid queryId, QueryResultTableDTO queryResultTableDTO);

    Task UserDefinedTypesWithDefaultsAndRulesAndDefinitionProcessReceivedDataAsync(Guid queryId, QueryResultTableDTO queryResultTableDTO);
    Task UserDefinedTableTypesProcessReceivedDataAsync(Guid queryId, QueryResultTableDTO queryResultTableDTO);
}

