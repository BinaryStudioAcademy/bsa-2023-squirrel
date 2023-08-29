using Squirrel.ConsoleApp.Models;

namespace Squirrel.ConsoleApp.Interfaces;

public interface IGetActionsService
{
    Task<QueryResultTable> GetAllTablesAsync();
    Task<QueryResultTable> GetTableDataAsync(string tableName, int rowsCount);

    Task<QueryResultTable> GetAllStoredProceduresAsync();
    Task<QueryResultTable> GetStoredProcedureDefinitionAsync(string storedProcedureName);

    Task<QueryResultTable> GetAllFunctionsAsync();
    Task<QueryResultTable> GetFunctionDefinitionAsync(string functionName);
}