namespace Squirrel.ConsoleApp.Interfaces;

public interface IDbQueryProvider
{
    string GetFunctionsQuery();
    string GetStoredProceduresQuery();
    string GetTablesQuery();
    string GetFunctionQuery(string functionName);
    string GetStoredProcedureQuery(string storedProcedureName);
    string GetTableDataQuery(string tableName, int rowsCount);
}
