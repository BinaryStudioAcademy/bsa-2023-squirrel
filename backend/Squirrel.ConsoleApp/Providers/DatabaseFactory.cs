using Squirrel.ConsoleApp.Interfaces;
using Squirrel.ConsoleApp.Models;
using Squirrel.ConsoleApp.Services;

namespace Squirrel.ConsoleApp.Providers;

public class DatabaseFactory
{
    public static IDatabaseService CreateDatabaseService(DatabaseType dbType, string connection)
    {
        return dbType switch
        {
            DatabaseType.SqlServer => new SqlServerService(connection),
            DatabaseType.PostgreSQL => new PostgreSqlService(connection),
            _ => throw new NotImplementedException($"Database type {dbType} is not supported."),
        };
    }
}