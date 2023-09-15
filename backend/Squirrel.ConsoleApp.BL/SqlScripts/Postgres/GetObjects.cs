namespace Squirrel.ConsoleApp.BL.SqlScripts.Postgres;

internal class GetObjects
{
    public static string GetStoredProceduresNamesScript =>
        @"SELECT routine_schema as ""Schema"", routine_name as ""ProcedureName"" FROM information_schema.routines 
            WHERE specific_schema not in ('information_schema', 'pg_catalog') an routine_type = 'PROCEDURE'";

    public static string GetStoredProcedureDefinitionScript(string storedProcedureSchema, string storedProcedureName) =>
        @$"SELECT routine_definition as ""RoutineDefinition"" FROM information_schema.routines 
            WHERE routine_schema = '{storedProcedureSchema}' AND routine_name = '{storedProcedureName}' AND routine_type = 'PROCEDURE'";

    public static string GetFunctionsNamesScript =>
        @"SELECT routine_schema as ""Schema"", routine_name as ""FunctionName"" FROM information_schema.routines 
            WHERE specific_schema not in ('information_schema', 'pg_catalog') and routine_type = 'FUNCTION'";

    public static string GetFunctionDefinitionScript(string functionSchema, string functionName) =>
        @$"SELECT routine_definition as ""RoutineDefinition"" FROM information_schema.routines 
            WHERE routine_schema = '{functionSchema}' AND routine_name = '{functionName}' AND routine_type = 'FUNCTION'";

    public static string GetViewsNamesScript =>
        @"SELECT table_schema AS ""Schema"", table_name AS ""ViewName""  FROM information_schema.views WHERE table_schema not in ('information_schema', 'pg_catalog')";

    public static string GetViewDefinitionScript(string viewSchema, string viewName) =>
        @$"SELECT view_definition as ""ViewDefinition"" FROM information_schema.views  WHERE table_schema = '{viewSchema}' AND table_name = '{viewName}'";

    public static string GetStoredProceduresScript =>
        @"
            select 
            routine_schema as ""Schema"", 
            routine_name as ""Name"",
            routine_type as ""RoutineType"",
            data_type as ""ReturnedType"",
            type_udt_schema as ""UdtTypeSchema"",
            type_udt_name as ""UdtTypeName"", 
            routine_definition as ""Definition""
            from information_schema.routines
            where routine_schema not in ('pg_catalog', 'information_schema')
                  and routine_type = 'PROCEDURE' 
            order by specific_schema
            ";

    public static string GetFunctionsScript =>
         @"
            select 
            routine_schema as ""Schema"", 
            routine_name as ""Name"",
            routine_type as ""RoutineType"",
            case when data_type = 'USER-DEFINED' then type_udt_name else data_type end as ""ReturnedType"",
            case when data_type = 'USER-DEFINED' then 'True' else 'Fasle' end as ""isUserDefined"", 
            routine_definition as ""Definition""
            from information_schema.routines
            where routine_schema not in ('pg_catalog', 'information_schema')
                  and routine_type = 'FUNCTION' 
            order by specific_schema
            ";

    public static string GetViewsScript =>
        @"
            select
          table_schema as ""Schema"", 
          table_name as ""Name"",
          view_definition as ""Definition""
            from information_schema.views
            where table_schema not in ('pg_catalog', 'information_schema')
            ";
}