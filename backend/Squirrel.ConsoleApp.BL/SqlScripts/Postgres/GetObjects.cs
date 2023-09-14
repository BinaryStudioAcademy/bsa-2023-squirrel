namespace Squirrel.ConsoleApp.BL.SqlScripts.Postgres;

internal class GetObjects
{
    public static string GetStoredProceduresNamesScript =>
        @"SELECT routine_name as ProcedureName FROM information_schema.routines WHERE routine_type = 'PROCEDURE'";

    public static string GetStoredProcedureDefinitionScript(string storedProcedureName) =>
        @$"SELECT * FROM information_schema.routines WHERE routine_name = '{storedProcedureName}' AND routine_type = 'PROCEDURE'";

    public static string GetFunctionsNamesScript =>
        @"SELECT routine_name as ProcedureName FROM information_schema.routines WHERE routine_type = 'FUNCTION'";

    public static string GetFunctionDefinitionScript(string functionName) =>
        @$"SELECT * FROM information_schema.routines WHERE routine_name = '{functionName}' AND routine_type = 'FUNCTION'";

    public static string GetViewsNamesScript =>
        @"SELECT table_name AS ViewName FROM information_schema.views";

    public static string GetViewDefinitionScript(string viewName) =>
        $"SELECT view_definition FROM information_schema.views WHERE table_name = '{viewName}'";

    public static string GetStoredProceduresScript =>
        @"
            select 
            routine_schema as schema, 
            routine_name as name,
            routine_type as routine_type,
            data_type as returned_type,
            type_udt_schema,
            type_udt_name, 
            routine_definition as definition
            from information_schema.routines
            where routine_type = 'PROCEDURE' 
            order by specific_schema
            ";

    public static string GetFunctionsScript =>
        @"
            select 
            routine_schema as schema, 
            routine_name as name,
            routine_type as routine_type,
            data_type as returned_type,
            type_udt_schema,
            type_udt_name, 
            routine_definition as definition
            from information_schema.routines
            where routine_type = ''FUNCTION'' 
            order by specific_schema
            ";

    public static string GetViewsScript =>
        @"
            select
	        table_schema as schema, 
	        table_name as view,
	        view_definition as definition
            from information_schema.views
            ";
}