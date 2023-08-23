using Squirrel.ConsoleApp.Models;
using Squirrel.ConsoleApp.Services.Abstract;
using System.Data.SqlClient;

namespace Squirrel.ConsoleApp.Services;

public class SqlServerService : BaseDbService
{
    public SqlServerService(string connectionString): base(connectionString) {}

    public override QueryResultTable ExecuteQuery(string query)
    {
        using var connection = new SqlConnection(ConnectionString);
        return ExecuteQueryFromConnectionInternal(connection, query);
    }

    public override async Task<QueryResultTable> ExecuteQueryAsync(string query)
    {
        using var connection = new SqlConnection(ConnectionString);
        return await ExecuteQueryFromConnectionInternalAsync(connection, query);
    }
}
