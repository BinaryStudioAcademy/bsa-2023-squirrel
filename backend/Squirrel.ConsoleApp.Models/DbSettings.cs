namespace Squirrel.ConsoleApp.Models;

public sealed class DbSettings
{
    public DbEngine DbType { get; set; }
    public string ConnectionString { get; set; } = string.Empty;
}
