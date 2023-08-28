using Squirrel.ConsoleApp.Models;

namespace Squirrel.ConsoleApp.Interfaces;

public interface IGetActionsService
{
    Task<IEnumerable<UserAction>> GetAllFunctionsAsync();
    Task<UserAction> GetFunctionAsync(string functionName);
    Task<IEnumerable<UserAction>> GetAllStoredProceduresAsync();
    Task<UserAction> GetStoredProcedureAsync(string storedProcedureName);
    Task<IEnumerable<UserAction>> GetAllTablesAsync();
    Task<UserAction> GetTableDataAsync(string tableName, int rowsCount);
}