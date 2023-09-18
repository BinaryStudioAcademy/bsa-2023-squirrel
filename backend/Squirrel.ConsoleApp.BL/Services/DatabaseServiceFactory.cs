using Squirrel.ConsoleApp.BL.Interfaces;
using Squirrel.ConsoleApp.BL.Providers;
using Squirrel.ConsoleApp.Models;

namespace Squirrel.ConsoleApp.BL.Services;

public class DatabaseServiceFactory
{
    public static IDatabaseService CreateDatabaseService(DbEngine dbType, string connection)
    {
        return dbType switch
        {
            DbEngine.SqlServer => new SqlServerService(connection),
            DbEngine.PostgreSql => new PostgreSqlService(connection),
            _ => new SqlServerService(connection)
        };
    }

    public static IDbQueryProvider CreateDbQueryProvider(DbEngine dbType)
    {
        return dbType switch
        {
            DbEngine.SqlServer => new SqlServerQueryProvider(),
            DbEngine.PostgreSql => new PostgreSqlQueryProvider(),
            _ => new SqlServerQueryProvider()
        };
    }
}