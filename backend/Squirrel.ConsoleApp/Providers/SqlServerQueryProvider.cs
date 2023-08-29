using Squirrel.ConsoleApp.Interfaces;
using static Squirrel.ConsoleApp.Providers.SqlServerScripts.GetTables;
using static Squirrel.ConsoleApp.Providers.SqlServerScripts.GetObjects;
using static Squirrel.ConsoleApp.Providers.SqlServerScripts.GetUserDefinedTypes;

namespace Squirrel.ConsoleApp.Providers;

public class SqlServerQueryProvider : IDbQueryProvider
{
    public string GetTablesNamesQuery() => GetTablesNamesScript;

    public string GetTableDataQuery(string tableName, int rowsCount)
    {
        string[] parts = tableName.Split('.');
        string schema = parts.Length > 1 ? parts[0] : "dbo";
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
