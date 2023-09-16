using Squirrel.SqlService.BLL.Models.ConsoleAppHub;

namespace Squirrel.SqlService.BLL.Interfaces.ConsoleAppHub;

public interface IProcessReceivedDataService
{
    // Actions
    Task AllTablesNamesProcessReceivedDataAsync(Guid httpId, QueryResultTableDTO queryResultTableDTO);
    Task TableDataProcessReceivedDataAsync(Guid httpId, QueryResultTableDTO queryResultTableDTO);

    Task AllStoredProceduresNamesProcessReceivedDataAsync(Guid httpId, QueryResultTableDTO queryResultTableDTO);
    Task StoredProcedureDefinitionProcessReceivedDataAsync(Guid httpId, QueryResultTableDTO queryResultTableDTO);

    Task AllFunctionsNamesProcessReceivedDataAsync(Guid httpId, QueryResultTableDTO queryResultTableDTO);
    Task FunctionDefinitionProcessReceivedDataAsync(Guid httpId, QueryResultTableDTO queryResultTableDTO);

    Task AllViewsNamesProcessReceivedDataAsync(Guid httpId, QueryResultTableDTO queryResultTableDTO);
    Task ViewDefinitionProcessReceivedDataAsync(Guid httpId, QueryResultTableDTO queryResultTableDTO);

    Task TableStructureProcessReceivedDataAsync(Guid httpId, QueryResultTableDTO queryResultTableDTO);
    Task TableChecksAndUniqueConstraintsProcessReceivedDataAsync(Guid httpId, QueryResultTableDTO queryResultTableDTO);

    Task StoredProceduresWithDetailProcessReceivedDataAsync(Guid httpId, QueryResultTableDTO queryResultTableDTO);
    Task FunctionsWithDetailProcessReceivedDataAsync(Guid httpId, QueryResultTableDTO queryResultTableDTO);
    Task ViewsWithDetailProcessReceivedDataAsync(Guid httpId, QueryResultTableDTO queryResultTableDTO);

    Task UserDefinedTypesWithDefaultsAndRulesAndDefinitionProcessReceivedDataAsync(Guid httpId, QueryResultTableDTO queryResultTableDTO);
    Task UserDefinedTableTypesProcessReceivedDataAsync(Guid httpId, QueryResultTableDTO queryResultTableDTO);
}

