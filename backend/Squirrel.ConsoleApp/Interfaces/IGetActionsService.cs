using Squirrel.ConsoleApp.Models;

namespace Squirrel.ConsoleApp.Interfaces;

public interface IGetActionsService
{
    Task<UserAction> GetAllTablesAsync();
    Task<TableData> GetTableDataAsync(string tableName, int rowsCount);

    Task<UserAction> GetAllFunctionsAsync();
    Task<UserAction> GetFunctionAsync(string functionName);

    Task<UserAction> GetAllStoredProceduresAsync();
    Task<UserAction> GetStoredProcedureAsync(string storedProcedureName);
}