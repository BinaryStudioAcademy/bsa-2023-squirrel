using Squirrel.ConsoleApp.Models;
using Squirrel.SqlService.BLL.Models.DTO;

namespace Squirrel.SqlService.BLL.Interfaces.ConsoleAppHub;

public interface IProcessReceivedDataService
{
    // Actions
    Task<TableNamesDto> AllTablesNamesProcessReceivedDataAsync(string clientId, QueryResultTable queryResultTable);
    Task TableDataProcessReceivedDataAsync(string clientId, QueryResultTable queryResultTable);

    Task AllStoredProceduresNamesProcessReceivedDataAsync(string clientId, QueryResultTable queryResultTable);
    Task StoredProcedureDefinitionProcessReceivedDataAsync(string clientId, QueryResultTable queryResultTable);

    Task AllFunctionsNamesProcessReceivedDataAsync(string clientId, QueryResultTable queryResultTable);
    Task FunctionDefinitionProcessReceivedDataAsync(string clientId, QueryResultTable queryResultTable);

    Task AllViewsNamesProcessReceivedDataAsync(string clientId, QueryResultTable queryResultTable);
    Task ViewDefinitionProcessReceivedDataAsync(string clientId, QueryResultTable queryResultTable);

    Task TableStructureProcessReceivedDataAsync(string clientId, QueryResultTable queryResultTable);
    Task TableChecksAndUniqueConstraintsProcessReceivedDataAsync(string clientId, QueryResultTable queryResultTable);

    Task StoredProceduresWithDetailProcessReceivedDataAsync(string clientId, QueryResultTable queryResultTable);
    Task FunctionsWithDetailProcessReceivedDataAsync(string clientId, QueryResultTable queryResultTable);
    Task ViewsWithDetailProcessReceivedDataAsync(string clientId, QueryResultTable queryResultTable);

    Task UserDefinedTypesWithDefaultsAndRulesAndDefinitionProcessReceivedDataAsync(string clientId, QueryResultTable queryResultTable);
    Task UserDefinedTableTypesProcessReceivedDataAsync(string clientId, QueryResultTable queryResultTable);
}

