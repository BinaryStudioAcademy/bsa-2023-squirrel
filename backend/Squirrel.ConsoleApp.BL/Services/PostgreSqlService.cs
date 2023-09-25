using Npgsql;
using Squirrel.ConsoleApp.BL.Services.Abstract;
using Squirrel.ConsoleApp.Models;

namespace Squirrel.ConsoleApp.BL.Services;

public class PostgreSqlService : BaseDbService
{
    public PostgreSqlService(string connectionString) : base(connectionString)
    {
    }

    public override QueryResultTable ExecuteQuery(string query)
    {
        using var connection = new NpgsqlConnection(ConnectionString);
        return ExecuteQueryFromConnectionInternal(connection, query);
    }

    public override async Task<QueryResultTable> ExecuteQueryAsync(string query)
    {
        using var connection = new NpgsqlConnection(ConnectionString);
        return await ExecuteQueryFromConnectionInternalAsync(connection, query);
    }
}
