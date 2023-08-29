namespace Squirrel.ConsoleApp.Interfaces;

public interface IDbQueryProvider
{
    string GetTablesNamesQuery();
    string GetTableDataQuery(string tableName, int rowsCount);

    string GetStoredProceduresNamesQuery();
    string GetStoredProcedureDefinitionQuery(string storedProcedureName);

    string GetFunctionsNamesQuery();
    string GetFunctionDefinitionQuery(string functionName);

    string GetViewsNamesQuery();
    string GetViewDefinitionQuery(string viewName);

    string GetTablesStructureQuery();
    string GetTablesCheckAndUniqueConstraintsQuery();

    string GetStoredProceduresWithDetailsQuery();
    string GetFunctionsWithDetailsQuery();
    string GetViewsWithDetailsQuery();

    string GetUserDefinedTypesWithDefaultsAndRulesAndDefinitionQuery();
    string GetUserDefinedTableTypesStructureQuery();
}
