namespace Squirrel.ConsoleApp.Interfaces;

public interface IDbQueryProvider
{
    string GetTablesQuery();
    string GetTableDataQuery(string tableName, int rowsCount);

    string GetStoredProceduresQuery();
    string GetStoredProcedureQuery(string storedProcedureName);

    string GetFunctionsQuery();
    string GetFunctionQuery(string functionName); 
}
