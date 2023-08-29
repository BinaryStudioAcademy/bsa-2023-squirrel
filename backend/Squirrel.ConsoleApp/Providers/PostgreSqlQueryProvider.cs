using Squirrel.ConsoleApp.Interfaces;

namespace Squirrel.ConsoleApp.Providers;

public class PostgreSqlQueryProvider : IDbQueryProvider
{
    public string GetTablesQuery()
    {
        return "SELECT tablename FROM pg_catalog.pg_tables WHERE schemaname != 'pg_catalog' AND schemaname != 'information_schema'";
    }

    public string GetTableDataQuery(string tableName, int rowsCount)
    {
        return $"SELECT * FROM {tableName} LIMIT {rowsCount}";
    }

    public string GetStoredProceduresQuery()
    {
        return @"SELECT routine_name as ProcedureName FROM information_schema.routines WHERE specific_schema='public' AND type_udt_name!='void'";
    }

    public string GetStoredProcedureDefinitionQuery(string storedProcedureName)
    {
        return $"SELECT * FROM information_schema.routines WHERE routine_name = '{storedProcedureName}' AND type_udt_name != 'void'";
    }

    public string GetFunctionsQuery()
    {
        return @"SELECT routine_name as ProcedureName FROM information_schema.routines WHERE specific_schema='public' AND type_udt_name = 'void'";
    }

    public string GetFunctionDefinitionQuery(string functionName)
    {
        return $"SELECT * FROM information_schema.routines WHERE routine_name = '{functionName}' AND type_udt_name = 'void'";
    }
}
