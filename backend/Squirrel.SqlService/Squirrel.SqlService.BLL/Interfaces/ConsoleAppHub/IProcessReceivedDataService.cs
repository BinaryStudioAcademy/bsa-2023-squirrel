using Squirrel.SqlService.BLL.Models.ConsoleAppHub;

namespace Squirrel.SqlService.BLL.Interfaces.ConsoleAppHub;

public interface IProcessReceivedDataService
{
    // Actions
    Task AllTablesNamesProcessReceivedDataAsync(string clientId, QueryResultTableDTO queryResultTableDTO);
    Task TableDataProcessReceivedDataAsync(string clientId, QueryResultTableDTO queryResultTableDTO);

    Task AllStoredProceduresNamesProcessReceivedDataAsync(string clientId, QueryResultTableDTO queryResultTableDTO);
    Task StoredProcedureDefinitionProcessReceivedDataAsync(string clientId, QueryResultTableDTO queryResultTableDTO);

    Task AllFunctionsNamesProcessReceivedDataAsync(string clientId, QueryResultTableDTO queryResultTableDTO);
    Task FunctionDefinitionProcessReceivedDataAsync(string clientId, QueryResultTableDTO queryResultTableDTO);

    Task AllViewsNamesProcessReceivedDataAsync(string clientId, QueryResultTableDTO queryResultTableDTO);
    Task ViewDefinitionProcessReceivedDataAsync(string clientId, QueryResultTableDTO queryResultTableDTO);

    Task TableStructureProcessReceivedDataAsync(string clientId, QueryResultTableDTO queryResultTableDTO);
    Task TableChecksAndUniqueConstraintsProcessReceivedDataAsync(string clientId, QueryResultTableDTO queryResultTableDTO);

    Task StoredProceduresWithDetailProcessReceivedDataAsync(string clientId, QueryResultTableDTO queryResultTableDTO);
    Task FunctionsWithDetailProcessReceivedDataAsync(string clientId, QueryResultTableDTO queryResultTableDTO);
    Task ViewsWithDetailProcessReceivedDataAsync(string clientId, QueryResultTableDTO queryResultTableDTO);

    Task UserDefinedTypesWithDefaultsAndRulesAndDefinitionProcessReceivedDataAsync(string clientId, QueryResultTableDTO queryResultTableDTO);
    Task UserDefinedTableTypesProcessReceivedDataAsync(string clientId, QueryResultTableDTO queryResultTableDTO);
}

