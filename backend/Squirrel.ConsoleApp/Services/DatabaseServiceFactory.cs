using Squirrel.ConsoleApp.BL.Interfaces;
using Squirrel.ConsoleApp.BL.Services;
using Squirrel.ConsoleApp.Models;
using Squirrel.ConsoleApp.Providers;

namespace Squirrel.ConsoleApp.Services;

public class DatabaseServiceFactory
{
    public static IDatabaseService CreateDatabaseService(DbEngine dbType, string connection)
    {
        return dbType switch
        {
            DbEngine.SqlServer => new SqlServerService(connection),
            DbEngine.PostgreSql => new PostgreSqlService(connection),
            _ => throw new NotImplementedException($"Database type {dbType} is not supported."),
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