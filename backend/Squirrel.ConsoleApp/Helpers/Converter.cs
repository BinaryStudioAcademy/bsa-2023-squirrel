using Squirrel.ConsoleApp.Models;

namespace Squirrel.ConsoleApp.Helpers;

public static class Converter
{
    public static ConnectionString DeserializeConnectionString(string[] args)
    {
        if (args.Length != 5)
        {
            throw new ArgumentException("Invalid ConnectionString");
        }

        DbType dbType = (DbType)Enum.Parse(typeof(DbType), args[4]);

        return new ConnectionString
        {
            ServerName = args[0],
            Port = int.Parse(args[1]),
            Username = args[2],
            Password = args[3],
            DbType = dbType
        };
    }
}