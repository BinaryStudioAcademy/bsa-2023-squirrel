using Squirrel.SqlService.BLL.Models.SquirrelHub;

namespace Squirrel.Core.BLL.Interfaces;

public interface ISquirrelHubToReceive
{
    // Actions
    Task ReceiveAllTablesNamesAsync(string clientId, QueryResultTableDTO queryResultTableDTO);
    Task ReceiveTableDataAsync(string clientId, QueryResultTableDTO queryResultTableDTO);

    Task ReceiveAllStoredProceduresNamesAsync(string clientId, QueryResultTableDTO queryResultTableDTO);
    Task ReceiveStoredProcedureDefinitionAsync(string clientId, QueryResultTableDTO queryResultTableDTO);

    Task ReceiveAllFunctionsNamesAsync(string clientId, QueryResultTableDTO queryResultTableDTO);
    Task ReceiveFunctionDefinitionAsync(string clientId, QueryResultTableDTO queryResultTableDTO);

    Task ReceiveAllViewsNamesAsync(string clientId, QueryResultTableDTO queryResultTableDTO);
    Task ReceiveViewDefinitionAsync(string clientId, QueryResultTableDTO queryResultTableDTO);

    Task ReceiveDbTablesStructureAsync(string clientId, QueryResultTableDTO queryResultTableDTO);
    Task ReceiveDbTablesCheckAndUniqueConstraintsAsync(string clientId, QueryResultTableDTO queryResultTableDTO);

    Task ReceiveStoredProceduresWithDetailAsync(string clientId, QueryResultTableDTO queryResultTableDTO);
    Task ReceiveFunctionsWithDetailAsync(string clientId, QueryResultTableDTO queryResultTableDTO);
    Task ReceiveViewsWithDetailAsync(string clientId, QueryResultTableDTO queryResultTableDTO);

    Task ReceiveUserDefinedTypesWithDefaultsAndRulesAndDefinitionAsync(string clientId, QueryResultTableDTO queryResultTableDTO);
    Task ReceiveUserDefinedTableTypesAsync(string clientId, QueryResultTableDTO queryResultTableDTO);
}

