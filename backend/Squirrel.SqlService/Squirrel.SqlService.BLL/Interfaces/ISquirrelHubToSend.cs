namespace Squirrel.Core.BLL.Interfaces;

public interface ISquirrelHubToSend
{
    Task SetClientId(string guid);
    

    // Actions
    Task GetAllTablesNamesAsync(string clientId);
    Task GetTableDataAsync(string clientId, string tableName, int rowsCount);

    Task GetAllStoredProceduresNamesAsync(string clientId);
    Task GetStoredProcedureDefinitionAsync(string clientId, string storedProcedureName);

    Task GetAllFunctionsNamesAsync(string clientId);
    Task GetFunctionDefinitionAsync(string clientId, string functionName);

    Task GetAllViewsNamesAsync(string clientId);
    Task GetViewDefinitionAsync(string clientId, string viewName);

    Task GetDbTablesStructureAsync(string clientId);
    Task GetDbTablesCheckAndUniqueConstraintsAsync(string clientId);

    Task GetStoredProceduresWithDetailAsync(string clientId);
    Task GetFunctionsWithDetailAsync(string clientId);
    Task GetViewsWithDetailAsync(string clientId);

    Task GetUserDefinedTypesWithDefaultsAndRulesAndDefinitionAsync(string clientId);
    Task GetUserDefinedTableTypesAsync(string clientId);
}

