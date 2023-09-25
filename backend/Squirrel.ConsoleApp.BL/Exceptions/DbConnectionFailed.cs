namespace Squirrel.ConsoleApp.BL.Exceptions;

public class DbConnectionFailed : Exception
{
    public DbConnectionFailed(string connectionString, string msg)
        : base($"Failed to connect to Database using connection string: {connectionString}. Details: {msg}")
    {
    }
}