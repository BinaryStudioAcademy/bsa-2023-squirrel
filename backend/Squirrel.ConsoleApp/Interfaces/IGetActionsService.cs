using Squirrel.ConsoleApp.Models;

namespace Squirrel.ConsoleApp.Interfaces;

public interface IGetActionsService
{
    Task<QueryResultTable> GetAllTablesNamesAsync();
    Task<QueryResultTable> GetTableDataAsync(string tableName, int rowsCount);

    Task<QueryResultTable> GetAllStoredProceduresNamesAsync();
    Task<QueryResultTable> GetStoredProcedureDefinitionAsync(string storedProcedureName);

    Task<QueryResultTable> GetAllFunctionsNamesAsync();
    Task<QueryResultTable> GetFunctionDefinitionAsync(string functionName);

    Task<QueryResultTable> GetAllViewsNamesAsync();
    Task<QueryResultTable> GetViewDefinitionAsync(string viewName);

    //Task<QueryResultTable> GetDbTablesStructureAsync();
    //Task<QueryResultTable> GetDbTablesCheckAndUniqueConstraintsAsync();
    //Task<QueryResultTable> GetStoredProceduresAsync();
    //Task<QueryResultTable> GetFunctionsAsync();
    //Task<QueryResultTable> GetViewsAsync();
}