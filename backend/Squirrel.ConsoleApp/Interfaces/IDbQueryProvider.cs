namespace Squirrel.ConsoleApp.Interfaces;

public interface IDbQueryProvider
{
    string GetTablesQuery();
    string GetTableDataQuery(string tableName, int rowsCount);

    string GetStoredProceduresQuery();
    string GetStoredProcedureDefinitionQuery(string storedProcedureName);

    string GetFunctionsQuery();
    string GetFunctionDefinitionQuery(string functionName);

    string GetViewsQuery();
    string GetViewDefinitionQuery(string functionName);
}
