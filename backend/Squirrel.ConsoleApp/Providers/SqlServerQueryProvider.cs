using Squirrel.ConsoleApp.Interfaces;

namespace Squirrel.ConsoleApp.Providers;

public class SqlServerQueryProvider : IDbQueryProvider
{
    public string GetTablesQuery()
    {
        return "SELECT TABLE_SCHEMA + '.' + TABLE_NAME AS FULL_TABLE_NAME FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_TYPE = 'BASE TABLE';";
    }

    public string GetTableDataQuery(string tableName, int rowsCount)
    {
        string[] parts = tableName.Split('.');
        string schema = parts.Length > 1 ? parts[0] : "dbo";
        string table = parts.Length > 1 ? parts[1] : tableName;
        return $"SELECT TOP {rowsCount} * FROM [{schema}].[{table}]";
    }

    public string GetStoredProceduresQuery()
    {
        return @"SELECT NAME as ProcedureName FROM SYS.OBJECTS WHERE TYPE_DESC = 'SQL_STORED_PROCEDURE'";
    }

    public string GetStoredProcedureDefinitionQuery(string storedProcedureName)
    {
        return $"SELECT ROUTINE_DEFINITION FROM INFORMATION_SCHEMA.ROUTINES WHERE ROUTINE_TYPE = 'PROCEDURE' AND ROUTINE_NAME = '{storedProcedureName}'";
    }

    public string GetFunctionsQuery()
    {
        return @"SELECT NAME as FunctionName FROM SYS.OBJECTS WHERE TYPE_DESC = 'SQL_SCALAR_FUNCTION' OR TYPE_DESC = 'SQL_TABLE_VALUED_FUNCTION' OR TYPE_DESC = 'SQL_INLINE_TABLE_VALUED_FUNCTION'";
    }

    public string GetFunctionDefinitionQuery(string functionName)
    {
        return $"SELECT ROUTINE_DEFINITION FROM INFORMATION_SCHEMA.ROUTINES WHERE ROUTINE_TYPE = 'FUNCTION' AND ROUTINE_NAME = '{functionName}'";
    }

    public string GetViewsQuery()
    {
        return $"SELECT NAME as ViewName FROM SYS.OBJECTS WHERE TYPE_DESC = 'VIEW'";
    }

    public string GetViewDefinitionQuery(string viewName)
    {
        return $"SELECT VIEW_DEFINITION FROM INFORMATION_SCHEMA.VIEWS WHERE TABLE_NAME = '{viewName}'";
    }
}
