namespace Squirrel.ConsoleApp.Providers.PostgreSqlScripts
{
    internal class GetObjects
    {
        public static string GetStoredProceduresNamesScript =>
            @"SELECT routine_name as ProcedureName FROM information_schema.routines WHERE specific_schema='public' AND type_udt_name!='void'";

        public static string GetStoredProcedureDefinitionScript(string storedProcedureName) =>
            @$"SELECT * FROM information_schema.routines WHERE routine_name = '{storedProcedureName}' AND type_udt_name != 'void'";

        public static string GetFunctionsNamesScript =>
            @"SELECT routine_name as ProcedureName FROM information_schema.routines WHERE specific_schema='public' AND type_udt_name = 'void'";

        public static string GetFunctionDefinitionScript(string functionName) =>
            @$"SELECT * FROM information_schema.routines WHERE routine_name = '{functionName}' AND type_udt_name = 'void'";

        public static string GetViewsNamesScript =>
            @"SELECT table_name AS ViewName FROM information_schema.views";

        public static string GetViewDefinitionScript(string viewName) =>
            $"SELECT view_definition FROM information_schema.views WHERE table_name = '{viewName}'";

        public static string GetStoredProceduresScript =>
            @"
             
            ";

        public static string GetFunctionsScript =>
            @"
            
            ";

        public static string GetViewsScript =>
            @"
            
            ";
    }
}