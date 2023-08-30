using Squirrel.ConsoleApp.BL.Interfaces;
using Squirrel.ConsoleApp.BL.Services;
using Squirrel.ConsoleApp.Models;

namespace Squirrel.ConsoleApp.Services;

public class DatabaseFactory
{
    public static IDatabaseService CreateDatabaseService(DbEngine dbType, string connection)
    {
        return dbType switch
        {
            DbEngine.SqlServer => new SqlServerService(connection),
            DbEngine.PostgreSQL => new PostgreSqlService(connection),
            _ => throw new NotImplementedException($"Database type {dbType} is not supported."),
        };
    }
}