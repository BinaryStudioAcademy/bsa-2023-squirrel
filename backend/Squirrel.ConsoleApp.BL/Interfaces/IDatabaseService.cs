using Squirrel.ConsoleApp.Models;

namespace Squirrel.ConsoleApp.BL.Interfaces;

public interface IDatabaseService
{
    string ConnectionString { get; }
    QueryResultTable ExecuteQuery(string query);
    Task<QueryResultTable> ExecuteQueryAsync(string query);
}
