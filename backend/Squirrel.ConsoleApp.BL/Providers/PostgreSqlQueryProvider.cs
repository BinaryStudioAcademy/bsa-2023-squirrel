using Squirrel.ConsoleApp.BL.Interfaces;
using static Squirrel.ConsoleApp.BL.SqlScripts.Postgres.GetTables;
using static Squirrel.ConsoleApp.BL.SqlScripts.Postgres.GetObjects;
using static Squirrel.ConsoleApp.BL.SqlScripts.Postgres.GetUserDefinedTypes;

namespace Squirrel.ConsoleApp.BL.Providers;

public class PostgreSqlQueryProvider : IDbQueryProvider
{
    public string GetTablesNamesQuery() => GetTablesNamesScript;

    public string GetTableDataQuery(string schema, string name, int rowsCount) => GetTableDataQueryScript(schema, name, rowsCount);

    public string GetStoredProceduresNamesQuery() => GetStoredProceduresNamesScript;

    public string GetStoredProcedureDefinitionQuery(string storedProcedureSchemaString, string storedProcedureName) => GetStoredProcedureDefinitionScript(storedProcedureSchemaString, storedProcedureName);

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
