using Squirrel.ConsoleApp.Models;

namespace Squirrel.ConsoleApp.BL.Interfaces;

public interface IDatabaseService
{
    string ConnectionString { get; }
    QueryResultTable ExecuteQuery(ParameterizedSqlCommand query);
    Task<QueryResultTable> ExecuteQueryAsync(ParameterizedSqlCommand query);
}
