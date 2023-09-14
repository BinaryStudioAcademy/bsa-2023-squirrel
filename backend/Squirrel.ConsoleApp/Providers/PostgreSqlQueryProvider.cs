using Squirrel.ConsoleApp.BL.Interfaces;
using static Squirrel.ConsoleApp.Providers.PostgreSqlScripts.GetTables;
using static Squirrel.ConsoleApp.Providers.PostgreSqlScripts.GetObjects;
using static Squirrel.ConsoleApp.Providers.PostgreSqlScripts.GetUserDefinedTypes;

namespace Squirrel.ConsoleApp.Providers;

public class PostgreSqlQueryProvider : IDbQueryProvider
{
    public string GetTablesNamesQuery() => GetTablesNamesScript;

    public string GetTableDataQuery(string schema, string name, int rowsCount) => GetTableDataQueryScript(schema, name, rowsCount);

    public string GetStoredProceduresNamesQuery() => GetStoredProceduresNamesScript;

    public string GetStoredProcedureDefinitionQuery(string storedProcedureSchema, string storedProcedureName) => GetStoredProcedureDefinitionScript(storedProcedureSchema, storedProcedureName);

    public string GetFunctionsNamesQuery() => GetFunctionsNamesScript;

    public string GetFunctionDefinitionQuery(string functionSchema, string functionName) => GetFunctionDefinitionScript(functionSchema, functionName);

    public string GetViewsNamesQuery() => GetViewsNamesScript;

    public string GetViewDefinitionQuery(string viewSchema, string viewName) => GetViewDefinitionScript(viewSchema, viewName);

    public string GetTableStructureQuery(string schema, string name) => GetTableStructureScript(schema, name);

    public string GetTableChecksAndUniqueConstraintsQuery(string schema, string name) => GetTableChecksAndUniqueConstraintsScript(schema, name);

    public string GetStoredProceduresWithDetailsQuery() => GetStoredProceduresScript;

    public string GetFunctionsWithDetailsQuery() => GetFunctionsScript;

    public string GetViewsWithDetailsQuery() => GetViewsScript;

    public string GetUserDefinedTypesWithDefaultsAndRulesAndDefinitionQuery() => GetUserDefinedTypesWithDefaultsAndRulesAndDefinitionScript;

    public string GetUserDefinedTableTypesStructureQuery() => GetUserDefinedTableTypesStructureScript;
}
