namespace Squirrel.ConsoleApp.BL.Interfaces;

public interface IDbQueryProvider
{
    string GetTablesNamesQuery();
    string GetTableDataQuery(string schema, string tableName, int rowsCount);

    string GetStoredProceduresNamesQuery();
    string GetStoredProcedureDefinitionQuery(string storedProcedureSchema, string storedProcedureName);

    string GetFunctionsNamesQuery();
    string GetFunctionDefinitionQuery(string functionSchema, string functionName);

    string GetViewsNamesQuery();
    string GetViewDefinitionQuery(string viewSchema, string viewName);

    string GetTableStructureQuery(string schema, string table);
    string GetTableChecksAndUniqueConstraintsQuery(string schema, string name);

    string GetStoredProceduresWithDetailsQuery();
    string GetFunctionsWithDetailsQuery();
    string GetViewsWithDetailsQuery();

    string GetUserDefinedTypesWithDefaultsAndRulesAndDefinitionQuery();
    string GetUserDefinedTableTypesStructureQuery();
}
