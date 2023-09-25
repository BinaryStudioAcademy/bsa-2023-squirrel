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

        connection.Close();

        return result;
    }

    private protected async Task<QueryResultTable> ExecuteQueryFromConnectionInternalAsync(DbConnection connection, string query)
    {
        using var command = CreateCommandInternal(connection, query);

        command.CommandTimeout = 45;

        await connection.OpenAsync();
        await using var reader = await command.ExecuteReaderAsync();

        var result = BuildTable(reader);

        await connection.CloseAsync();

        return result;
    }

    private QueryResultTable BuildTable(DbDataReader reader)
    {
        // to display 'null' in empty cells in results table on UI
        const string nullValue = "null";

        var fieldCount = reader.FieldCount;
        var columnNames = Enumerable.Range(0, fieldCount).Select(reader.GetName).ToArray();
        var result = new QueryResultTable(columnNames);

        while (reader.Read())
        {
            var row = new List<string>(fieldCount);
            for (int i = 0; i < fieldCount; i++)
            {
                var value = reader.IsDBNull(i) ? nullValue : reader[i].ToString();
                row.Add(value ?? nullValue);
            }
            result.AddRow(row.ToArray());
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
