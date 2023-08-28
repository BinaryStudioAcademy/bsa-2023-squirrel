using Squirrel.ConsoleApp.Interfaces;
using Squirrel.ConsoleApp.Models;

namespace Squirrel.ConsoleApp.Services;

public class DatabaseFactory
{
    public static IDatabaseService CreateDatabaseService(DbType dbType, string connection)
    {
        return dbType switch
        {
            DbType.SqlServer => new SqlServerService(connection),
            DbType.PostgreSQL => new PostgreSqlService(connection),
            _ => throw new NotImplementedException($"Database type {dbType} is not supported."),
        };
    }

    public static string GetFunctionsQuery(DbType dbType)
    {
        return dbType switch
        {
            DbType.SqlServer => @"SELECT NAME as FunctionName FROM SYS.OBJECTS WHERE TYPE_DESC = 'SQL_SCALAR_FUNCTION' OR TYPE_DESC = 'SQL_TABLE_VALUED_FUNCTION'",
            DbType.PostgreSQL => @"SELECT routine_name as FunctionName FROM information_schema.routines WHERE specific_schema='public' AND type_udt_name='void'",
            _ => throw new NotImplementedException($"Database type {dbType} is not supported.")
        };
    }

    public static string GetStoredProceduresQuery(DbType dbType)
    {
        return dbType switch
        {
            DbType.SqlServer => @"SELECT NAME as ProcedureName FROM SYS.OBJECTS WHERE TYPE_DESC = 'SQL_STORED_PROCEDURE'",
            DbType.PostgreSQL => @"SELECT routine_name as ProcedureName FROM information_schema.routines WHERE specific_schema='public' AND type_udt_name!='void'",
            _ => throw new NotImplementedException($"Database type {dbType} is not supported.")
        };
    }

    public static string GetTablesQuery(DbType dbType)
    {
        return dbType switch
        {
            DbType.SqlServer => "SELECT TABLE_NAME FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_TYPE='BASE TABLE'",
            DbType.PostgreSQL => "SELECT tablename FROM pg_catalog.pg_tables WHERE schemaname != 'pg_catalog' AND schemaname != 'information_schema'",
            _ => throw new NotImplementedException($"Database type {dbType} is not supported.")
        };
    }

    public static string GetFunctionQuery(DbType dbType, string functionName)
    {
        return dbType switch
        {
            DbType.SqlServer => $"SELECT * FROM INFORMATION_SCHEMA.ROUTINES WHERE ROUTINE_TYPE = 'FUNCTION' AND ROUTINE_NAME = '{functionName}'",
            DbType.PostgreSQL => $"SELECT * FROM information_schema.routines WHERE routine_name = '{functionName}' AND type_udt_name = 'void'",
            _ => throw new NotImplementedException($"Database type {dbType} is not supported.")
        };
    }

    public static string GetStoredProcedureQuery(DbType dbType, string storedProcedureName)
    {
        return dbType switch
        {
            DbType.SqlServer => $"SELECT * FROM INFORMATION_SCHEMA.ROUTINES WHERE ROUTINE_TYPE = 'PROCEDURE' AND ROUTINE_NAME = '{storedProcedureName}'",
            DbType.PostgreSQL => $"SELECT * FROM information_schema.routines WHERE routine_name = '{storedProcedureName}' AND type_udt_name != 'void'",
            _ => throw new NotImplementedException($"Database type {dbType} is not supported.")
        };
    }

    public static string GetTableDataQuery(DbType dbType, string tableName, int rowsCount)
    {
        return dbType switch
        {
            DbType.SqlServer => $"SELECT TOP {rowsCount} * FROM {tableName}",
            DbType.PostgreSQL => $"SELECT * FROM {tableName} LIMIT {rowsCount}",
            _ => throw new NotImplementedException($"Database type {dbType} is not supported.")
        };
    }
}