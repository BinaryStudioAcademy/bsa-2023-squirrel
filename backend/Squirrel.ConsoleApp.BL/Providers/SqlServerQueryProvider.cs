using System.Data;
using System.Data.SqlClient;
using Squirrel.ConsoleApp.BL.Interfaces;
using Squirrel.ConsoleApp.Models;
using static Squirrel.ConsoleApp.BL.SqlScripts.SqlServer.GetTables;
using static Squirrel.ConsoleApp.BL.SqlScripts.SqlServer.GetObjects;
using static Squirrel.ConsoleApp.BL.SqlScripts.SqlServer.GetUserDefinedTypes;

namespace Squirrel.ConsoleApp.BL.Providers;

public class SqlServerQueryProvider : BaseProvider, IDbQueryProvider
{
    public ParameterizedSqlCommand GetTablesNamesQuery() => new(GetTablesNamesScript, new List<SqlParameter>());

    public ParameterizedSqlCommand GetTableDataQuery(string schema, string name, int rowsCount)
    {
        var schemaParameter = GetParameter(nameof(schema), schema, SqlDbType.NVarChar);
        var nameParameter = GetParameter(nameof(name), name, SqlDbType.NVarChar);
        var rowsParameter = GetParameter(nameof(rowsCount), rowsCount, SqlDbType.Int);
        
        return new ParameterizedSqlCommand(GetTableDataQueryScript(schema, name, rowsCount), new[]
        {
            schemaParameter, nameParameter, rowsParameter
        });
    }

    public ParameterizedSqlCommand GetStoredProceduresNamesQuery() => new(GetStoredProceduresNamesScript, new List<SqlParameter>());

    public ParameterizedSqlCommand GetStoredProcedureDefinitionQuery(string storedProcedureSchemaString,
        string storedProcedureName)
    {
        var storedProcedureSchemaStringParameter = GetParameter(
            nameof(storedProcedureSchemaString), storedProcedureSchemaString, SqlDbType.NVarChar);
        var storedProcedureNameParameter = GetParameter(
            nameof(storedProcedureName), storedProcedureName, SqlDbType.NVarChar);
        
        return new ParameterizedSqlCommand(GetStoredProcedureDefinitionScript(storedProcedureSchemaString, storedProcedureName), new[]
        {
            storedProcedureSchemaStringParameter, storedProcedureNameParameter
        });
    }

    public ParameterizedSqlCommand GetFunctionsNamesQuery() => new(GetFunctionsNamesScript, new List<SqlParameter>());

    public ParameterizedSqlCommand GetFunctionDefinitionQuery(string functionSchema, string functionName)
    {
        var functionSchemaParameter = GetParameter(nameof(functionSchema), functionSchema, SqlDbType.NVarChar);
        var functionNameParameter = GetParameter(nameof(functionName), functionName, SqlDbType.NVarChar);
        
        return new ParameterizedSqlCommand(GetFunctionDefinitionScript(functionSchema, functionName), new[]
        {
            functionSchemaParameter, functionNameParameter
        });
    }

    public ParameterizedSqlCommand GetViewsNamesQuery() => new(GetViewsNamesScript, new List<SqlParameter>());

    public ParameterizedSqlCommand GetViewDefinitionQuery(string viewSchema, string viewName)
    {
        var viewSchemaParameter = GetParameter(nameof(viewSchema), viewSchema, SqlDbType.NVarChar);
        var viewNameParameter = GetParameter(nameof(viewName), viewName, SqlDbType.NVarChar);
        
        return new ParameterizedSqlCommand(GetViewDefinitionScript(viewSchema, viewName), new[]
        {
            viewSchemaParameter, viewNameParameter
        });
    }

    public ParameterizedSqlCommand GetTableStructureQuery(string schema, string name)
    {
        var schemaParameter = GetParameter(nameof(schema), schema, SqlDbType.NVarChar);
        var nameParameter = GetParameter(nameof(name), name, SqlDbType.NVarChar);
        
        return new ParameterizedSqlCommand(GetTableStructureScript(schema, name), new[]
        {
            schemaParameter, nameParameter
        });
    }

    public ParameterizedSqlCommand GetTableChecksAndUniqueConstraintsQuery(string schema, string name)
    {
        var schemaParameter = GetParameter(nameof(schema), schema, SqlDbType.NVarChar);
        var nameParameter = GetParameter(nameof(name), name, SqlDbType.NVarChar);
        
        return new ParameterizedSqlCommand(GetTableChecksAndUniqueConstraintsScript(schema, name), new[]
        {
            schemaParameter, nameParameter
        });
    }

    public ParameterizedSqlCommand GetStoredProceduresWithDetailsQuery() => new(GetStoredProceduresScript, new List<SqlParameter>());

    public ParameterizedSqlCommand GetFunctionsWithDetailsQuery() => new(GetFunctionsScript, new List<SqlParameter>());

    public ParameterizedSqlCommand GetViewsWithDetailsQuery() => new(GetViewsScript, new List<SqlParameter>());

    public ParameterizedSqlCommand GetUserDefinedTypesWithDefaultsAndRulesAndDefinitionQuery()
        => new(GetUserDefinedTypesWithDefaultsAndRulesAndDefinitionScript, new List<SqlParameter>());

    public ParameterizedSqlCommand GetUserDefinedTableTypesStructureQuery()
        => new(GetUserDefinedTableTypesStructureScript, new List<SqlParameter>());
}
