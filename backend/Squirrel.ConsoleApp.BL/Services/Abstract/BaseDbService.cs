using Squirrel.ConsoleApp.BL.Interfaces;
using Squirrel.ConsoleApp.Models;
using System.Data.Common;

namespace Squirrel.ConsoleApp.BL.Services.Abstract;
public abstract class BaseDbService : IDatabaseService
{
    public string ConnectionString { get; set; }

    public BaseDbService(string connectionString)
    {
        ConnectionString = connectionString;
    }

    public abstract QueryResultTable ExecuteQuery(string query);
    public abstract Task<QueryResultTable> ExecuteQueryAsync(string query);

    private protected QueryResultTable ExecuteQueryFromConnectionInternal(DbConnection connection, string query)
    {
        using var command = CreateCommandInternal(connection, query);

        connection.Open();
        using var reader = command.ExecuteReader();

        var result = BuildTable(reader);

        return result;
    }

    private protected async Task<QueryResultTable> ExecuteQueryFromConnectionInternalAsync(DbConnection connection, string query)
    {
        using var command = CreateCommandInternal(connection, query);

        command.CommandTimeout = 30;

        await connection.OpenAsync();
        await using var reader = await command.ExecuteReaderAsync();

        var result = BuildTable(reader);

        return result;
    }

    private QueryResultTable BuildTable(DbDataReader reader)
    {
        var result = new QueryResultTable(Enumerable.Range(0, reader.FieldCount).Select(reader.GetName).ToArray());
        while (reader.Read())
        {
            var row = new string[reader.FieldCount];
            for (int i = 0; i < reader.FieldCount; i++)
            {
                row[i] = reader[i].ToString() ?? string.Empty;
            }
            result.AddRow(row);
        }
        return result;
    }

    private DbCommand CreateCommandInternal(DbConnection connection, string query)
    {
        var command = connection.CreateCommand();
        command.CommandText = query;
        return command;
    }
}
