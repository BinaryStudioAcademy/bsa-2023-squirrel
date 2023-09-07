using Squirrel.ConsoleApp.BL.Interfaces;
using static Squirrel.ConsoleApp.Providers.PostgreSqlScripts.GetTables;
using static Squirrel.ConsoleApp.Providers.PostgreSqlScripts.GetObjects;
using static Squirrel.ConsoleApp.Providers.PostgreSqlScripts.GetUserDefinedTypes;

namespace Squirrel.ConsoleApp.Providers;

public class PostgreSqlQueryProvider : IDbQueryProvider
{
    public string GetTablesNamesQuery() => GetTablesNamesScript;

    public string GetTableDataQuery(string tableName, int rowsCount)
    {
        string[] parts = tableName.Split('.');
        string schema = parts.Length > 1 ? parts[0] : "public";
        string table = parts.Length > 1 ? parts[1] : tableName;
        return GetTableDataQueryScript(rowsCount, schema, table);
    }

    public string GetStoredProceduresNamesQuery() => GetStoredProceduresNamesScript;

    public string GetStoredProcedureDefinitionQuery(string storedProcedureName) => GetStoredProcedureDefinitionScript(storedProcedureName);

    public string GetFunctionsNamesQuery() => GetFunctionsNamesScript;

    public string GetFunctionDefinitionQuery(string functionName) => GetFunctionDefinitionScript(functionName);

    public string GetViewsNamesQuery() => GetViewsNamesScript;

    public string GetViewDefinitionQuery(string viewName) => GetViewDefinitionScript(viewName);

    public string GetTablesStructureQuery() => GetTablesStructureScript;

    public string GetTablesCheckAndUniqueConstraintsQuery() => GetDbTablesCheckAndUniqueConstraintsScript;

    public string GetStoredProceduresWithDetailsQuery() => GetStoredProceduresScript;

    public string GetFunctionsWithDetailsQuery() => GetFunctionsScript;

    public string GetViewsWithDetailsQuery() => GetViewsScript;

    public string GetUserDefinedTypesWithDefaultsAndRulesAndDefinitionQuery() => GetUserDefinedTypesWithDefaultsAndRulesAndDefinitionScript;

    public string GetUserDefinedTableTypesStructureQuery() => GetUserDefinedTableTypesStructureScript;
}
