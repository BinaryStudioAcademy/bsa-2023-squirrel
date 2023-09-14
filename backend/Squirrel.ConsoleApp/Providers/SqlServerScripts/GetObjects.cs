namespace Squirrel.ConsoleApp.Providers.SqlServerScripts
{
    internal class GetObjects
    {
        public static string GetStoredProceduresNamesScript =>
            @"SELECT NAME as ProcedureName FROM SYS.OBJECTS WHERE TYPE_DESC = 'SQL_STORED_PROCEDURE'";

        public static string GetStoredProcedureDefinitionScript(string storedProcedureSchema, string storedProcedureName) =>
            @$"
                SELECT M.definition [ROUTINE_DEFINITION] 

                FROM INFORMATION_SCHEMA.ROUTINES R INNER JOIN sys.sql_modules M ON M.object_id = OBJECT_ID(R.ROUTINE_NAME)

                WHERE ROUTINE_TYPE = 'PROCEDURE' AND ROUTINE_NAME = '{storedProcedureName}'
            ";

        public static string GetFunctionsNamesScript =>
            @"SELECT NAME as FunctionName FROM SYS.OBJECTS WHERE TYPE_DESC = 'SQL_SCALAR_FUNCTION' OR TYPE_DESC = 'SQL_TABLE_VALUED_FUNCTION' OR TYPE_DESC = 'SQL_INLINE_TABLE_VALUED_FUNCTION'";

        public static string GetFunctionDefinitionScript(string functionSchema, string functionName) =>
            @$"
                SELECT M.definition [ROUTINE_DEFINITION] 

                FROM INFORMATION_SCHEMA.ROUTINES R INNER JOIN sys.sql_modules M ON M.object_id = OBJECT_ID(R.ROUTINE_NAME)

                WHERE ROUTINE_TYPE = 'FUNCTION' AND ROUTINE_NAME = '{functionName}'
            ";

        public static string GetViewsNamesScript =>
            @$"SELECT NAME as ViewName FROM SYS.OBJECTS WHERE TYPE_DESC = 'VIEW'";

        public static string GetViewDefinitionScript(string viewSchema, string viewName) =>
            @$"
                SELECT M.definition [VIEW_DEFINITION] 

                FROM INFORMATION_SCHEMA.VIEWS V INNER JOIN sys.sql_modules M ON M.object_id = OBJECT_ID(V.TABLE_NAME)

                WHERE TABLE_NAME = '{viewName}'
            ";

        public static string GetStoredProceduresScript =>
            @"
             SELECT ROUTINE_CATALOG [RoutineCatalog],
		            ROUTINE_SCHEMA [RoutineSchema],
		            ROUTINE_NAME [RoutineName],
		            M.definition [RoutineDefinition]

            FROM	INFORMATION_SCHEMA.ROUTINES R INNER JOIN sys.sql_modules M ON M.object_id = OBJECT_ID(R.ROUTINE_NAME)

            WHERE	ROUTINE_TYPE = 'PROCEDURE'
            ";

        public static string GetFunctionsScript =>
            @"
            SELECT ROUTINE_CATALOG [RoutineCatalog],
		            ROUTINE_SCHEMA [RoutineSchema],
		            ROUTINE_NAME [RoutineName],
		            DATA_TYPE [ReturnDataType],
		            CHARACTER_MAXIMUM_LENGTH [ReturnCharacterMaxLength],
		            NUMERIC_PRECISION [ReturnNumericPrecision],
		            NUMERIC_SCALE [ReturnNumericScale],
		            M.definition [RoutineDefinition]

            FROM	INFORMATION_SCHEMA.ROUTINES R INNER JOIN sys.sql_modules M ON M.object_id = OBJECT_ID(R.ROUTINE_NAME)

            WHERE	ROUTINE_TYPE = 'FUNCTION'
            ";

        public static string GetViewsScript =>
            @"
            SELECT TABLE_SCHEMA [ViewSchema],
		            TABLE_NAME [ViewName],
		            M.definition [ViewDefinition]

            FROM	INFORMATION_SCHEMA.VIEWS V

		    INNER JOIN sys.sql_modules M ON M.object_id = OBJECT_ID(V.TABLE_NAME)
            ";
    }
}