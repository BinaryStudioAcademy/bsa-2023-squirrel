using Squirrel.ConsoleApp.Models;

namespace Squirrel.ConsoleApp.BL.Interfaces;

public interface IGetActionsService
{
    Task<QueryResultTable> TestExecuteQueryAsync(string filterName, int filterRowsCount);

    Task<QueryResultTable> GetAllTablesNamesAsync();
    Task<QueryResultTable> GetTableDataAsync(string tableName, int rowsCount);

    Task<QueryResultTable> GetAllStoredProceduresNamesAsync();
    Task<QueryResultTable> GetStoredProcedureDefinitionAsync(string storedProcedureName);

    Task<QueryResultTable> GetAllFunctionsNamesAsync();
    Task<QueryResultTable> GetFunctionDefinitionAsync(string functionName);

    Task<QueryResultTable> GetAllViewsNamesAsync();
    Task<QueryResultTable> GetViewDefinitionAsync(string viewName);

    Task<QueryResultTable> GetDbTablesStructureAsync();
    Task<QueryResultTable> GetDbTablesCheckAndUniqueConstraintsAsync();

    Task<QueryResultTable> GetStoredProceduresWithDetailAsync();
    Task<QueryResultTable> GetFunctionsWithDetailAsync();
    Task<QueryResultTable> GetViewsWithDetailAsync();

    Task<QueryResultTable> GetUserDefinedTypesWithDefaultsAndRulesAndDefinitionAsync();
    Task<QueryResultTable> GetUserDefinedTableTypesAsync();
}