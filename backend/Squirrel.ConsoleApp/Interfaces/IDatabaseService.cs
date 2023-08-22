namespace Squirrel.ConsoleApp.Interfaces
{
    public interface IDatabaseService
    {
        string ConnectionString { get; }
        string ExecuteQuery(string query);
        Task<string> ExecuteQueryAsync(string query);
    }
}
