namespace Squirrel.ConsoleApp.Providers.PostgreSqlScripts
{
    internal static class GetTables
    {
        public static string GetTablesNamesScript =>
            @"SELECT schemaname || '.' || tablename AS full_table_name FROM pg_catalog.pg_tables WHERE schemaname != 'pg_catalog' AND schemaname != 'information_schema';";

        public static string GetTableDataQueryScript(int rowsCount, string schema, string table) =>
            $"SELECT * FROM \"{schema}\".\"{table}\" LIMIT {rowsCount}";

        public static string GetTablesStructureScript =>
            @"
            TBD
            ";

        public static string GetDbTablesCheckAndUniqueConstraintsScript =>
            @"
            TBD
            ";
    }
}