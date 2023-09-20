namespace Squirrel.ConsoleApp.BL.SqlScripts.SqlServer;

internal class GetObjects
{
    public static string GetStoredProceduresNamesScript =>
        @"SELECT SCHEMA_NAME(schema_id) AS Schema, NAME as Name FROM SYS.OBJECTS WHERE TYPE_DESC = 'SQL_STORED_PROCEDURE'";

    public static string GetStoredProcedureDefinitionScript(string storedProcedureSchema, string storedProcedureName) =>
        @$"
                SELECT M.definition [Definition] 

                FROM INFORMATION_SCHEMA.ROUTINES R INNER JOIN sys.sql_modules M ON M.object_id = OBJECT_ID(R.ROUTINE_NAME)

                WHERE ROUTINE_SCHEMA = '{storedProcedureSchema}' AND ROUTINE_TYPE = 'PROCEDURE' AND ROUTINE_NAME = '{storedProcedureName}'
            ";

    public static string GetFunctionsNamesScript =>
        @"SELECT SCHEMA_NAME(schema_id) AS Schema, NAME as Name FROM SYS.OBJECTS 
            WHERE TYPE_DESC = 'SQL_SCALAR_FUNCTION' OR TYPE_DESC = 'SQL_TABLE_VALUED_FUNCTION' OR TYPE_DESC = 'SQL_INLINE_TABLE_VALUED_FUNCTION'";

    public static string GetFunctionDefinitionScript(string functionSchema, string functionName) =>
        @$"
                SELECT M.definition [Definition] 

                FROM INFORMATION_SCHEMA.ROUTINES R INNER JOIN sys.sql_modules M ON M.object_id = OBJECT_ID(R.ROUTINE_NAME)

                WHERE ROUTINE_SCHEMA = '{functionSchema}' AND ROUTINE_TYPE = 'FUNCTION' AND ROUTINE_NAME = '{functionName}'
            ";

    public static string GetViewsNamesScript =>
        @$"SELECT SCHEMA_NAME(schema_id) AS Schema, NAME as Name FROM SYS.OBJECTS WHERE TYPE_DESC = 'VIEW'";

    public static string GetViewDefinitionScript(string viewSchema, string viewName) =>
        @$"
                SELECT M.definition [Definition] 

                FROM INFORMATION_SCHEMA.VIEWS V INNER JOIN sys.sql_modules M ON M.object_id = OBJECT_ID(V.TABLE_NAME)

                WHERE V.TABLE_SCHEMA = '{viewSchema}' AND TABLE_NAME = '{viewName}'
            ";

    public static string GetStoredProceduresScript =>
        @"
                SELECT ROUTINE_SCHEMA [Schema],
		               ROUTINE_NAME [Name],
		               M.definition [Definition]

                FROM INFORMATION_SCHEMA.ROUTINES R INNER JOIN sys.sql_modules M ON M.object_id = OBJECT_ID(R.ROUTINE_NAME)

                WHERE ROUTINE_TYPE = 'PROCEDURE'
            ";

    public static string GetFunctionsScript =>
        @"
                SELECT ROUTINE_SCHEMA [Schema],
		               ROUTINE_NAME [Name],
		               DATA_TYPE [ReturnedType],
		               CASE WHEN DATA_TYPE = 'TABLE' THEN 'True' ELSE 'False' END [IsUserDefined],
		               M.definition [Definition]
                FROM INFORMATION_SCHEMA.ROUTINES R
		        INNER JOIN sys.sql_modules M ON M.object_id = OBJECT_ID(R.ROUTINE_NAME)
                WHERE ROUTINE_TYPE = 'FUNCTION'
            ";

    public static string GetViewsScript =>
        @"
                SELECT TABLE_SCHEMA [Schema],
		               TABLE_NAME [Name],
		               M.definition [Definition]
                FROM INFORMATION_SCHEMA.VIEWS V
		        INNER JOIN sys.sql_modules M ON M.object_id = OBJECT_ID(V.TABLE_NAME)
            ";
}