using Squirrel.ConsoleApp.Interfaces;

namespace Squirrel.ConsoleApp.Providers;

public class SqlServerQueryProvider : IDbQueryProvider
{
    public string GetFunctionsQuery()
    {
        return @"SELECT NAME as FunctionName FROM SYS.OBJECTS WHERE TYPE_DESC = 'SQL_SCALAR_FUNCTION' OR TYPE_DESC = 'SQL_TABLE_VALUED_FUNCTION'";
    }

    public string GetStoredProceduresQuery()
    {
        return @"SELECT NAME as ProcedureName FROM SYS.OBJECTS WHERE TYPE_DESC = 'SQL_STORED_PROCEDURE'";
    }

    public string GetTablesQuery()
    {
        return "SELECT TABLE_NAME FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_TYPE='BASE TABLE'";
    }

    public string GetFunctionQuery(string functionName)
    {
        return $"SELECT ROUTINE_DEFINITION FROM INFORMATION_SCHEMA.ROUTINES WHERE ROUTINE_TYPE = 'FUNCTION' AND ROUTINE_NAME = '{functionName}'";
    }

    public string GetStoredProcedureQuery(string storedProcedureName)
    {
        return $"SELECT ROUTINE_DEFINITION FROM INFORMATION_SCHEMA.ROUTINES WHERE ROUTINE_TYPE = 'PROCEDURE' AND ROUTINE_NAME = '{storedProcedureName}'";
    }

    public string GetTableDataQuery(string tableName, int rowsCount)
    {
        return $"SELECT TOP {rowsCount} * FROM {tableName}";
    }
}
