namespace Squirrel.ConsoleApp.Models;

public class ConnectionStringDto
{
    public string DbName { get; set; } = string.Empty;
    public string? ServerName { get; set; }
    public int Port { get; set; }
    public string? Username { get; set; }
    public string? Password { get; set; }
    public DbEngine DbEngine { get; set; }

    public override string ToString()
    {
        return
            $"{nameof(ServerName)}: {ServerName}, {nameof(Port)}: {Port}, {nameof(Username)}: {Username}, " +
            $"{nameof(Password)}: {Password}, {nameof(DbEngine)}: {DbEngine}";
    }
}