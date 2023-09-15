using Squirrel.ConsoleApp.Models;
using Squirrel.SqlService.BLL.Models.DTO;
using Squirrel.SqlService.BLL.Models.DTO.Function;
using Squirrel.SqlService.BLL.Models.DTO.Procedure;
using Squirrel.SqlService.BLL.Models.DTO.Shared;

namespace Squirrel.SqlService.BLL.Interfaces.ConsoleAppHub;

public interface IProcessReceivedDataService
{
    // Actions
    Task<TableNamesDto> AllTablesNamesProcessReceivedDataAsync(string clientId, QueryResultTable queryResultTable);
    Task<TableDataDto> TableDataProcessReceivedDataAsync(string clientId, QueryResultTable queryResultTable);
    Task<TableStructureDto> TableStructureProcessReceivedDataAsync(string clientId, QueryResultTable queryResultTable);
    Task<TableConstraintsDto> TableChecksAndUniqueConstraintsProcessReceivedDataAsync(string clientId, QueryResultTable queryResultTable);

    Task<ProcedureNamesDto> AllStoredProceduresNamesProcessReceivedDataAsync(string clientId, QueryResultTable queryResultTable);
    Task<RoutineDefinitionDto> StoredProcedureDefinitionProcessReceivedDataAsync(string clientId, QueryResultTable queryResultTable);

    Task<FunctionNamesDto> AllFunctionsNamesProcessReceivedDataAsync(string clientId, QueryResultTable queryResultTable);
    Task<RoutineDefinitionDto> FunctionDefinitionProcessReceivedDataAsync(string clientId, QueryResultTable queryResultTable);

    Task AllViewsNamesProcessReceivedDataAsync(string clientId, QueryResultTable queryResultTable);
    Task ViewDefinitionProcessReceivedDataAsync(string clientId, QueryResultTable queryResultTable);

    Task<ProcedureDetailsDto> StoredProceduresWithDetailProcessReceivedDataAsync(string clientId, QueryResultTable queryResultTable);
    Task<FunctionDetailsDto> FunctionsWithDetailProcessReceivedDataAsync(string clientId, QueryResultTable queryResultTable);
    Task ViewsWithDetailProcessReceivedDataAsync(string clientId, QueryResultTable queryResultTable);

    Task UserDefinedTypesWithDefaultsAndRulesAndDefinitionProcessReceivedDataAsync(string clientId, QueryResultTable queryResultTable);
    Task UserDefinedTableTypesProcessReceivedDataAsync(string clientId, QueryResultTable queryResultTable);
}