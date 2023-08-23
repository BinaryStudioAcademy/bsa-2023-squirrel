using Npgsql;
using Squirrel.ConsoleApp.Services.Abstract;

namespace Squirrel.ConsoleApp.Services;

public class PostgreSqlService : BaseDbService
{
    public PostgreSqlService(string connectionString): base(connectionString) {}

    public override string ExecuteQuery(string query)
    {
        using var connection = new NpgsqlConnection(ConnectionString);
        return ExecuteQueryFromConnectionInternal(connection, query);
    }

    public override async Task<string> ExecuteQueryAsync(string query)
    {
        using var connection = new NpgsqlConnection(ConnectionString);
        return await ExecuteQueryFromConnectionInternalAsync(connection, query);
    }
}
