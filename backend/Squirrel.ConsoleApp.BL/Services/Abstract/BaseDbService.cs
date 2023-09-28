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

    public abstract QueryResultTable ExecuteQuery(ParameterizedSqlCommand query);
    
    public abstract Task<QueryResultTable> ExecuteQueryAsync(ParameterizedSqlCommand query);

    private protected QueryResultTable ExecuteQueryFromConnectionInternal(DbConnection connection, ParameterizedSqlCommand query)
    {
        using var command = CreateCommandInternal(connection, query);

        connection.Open();
        
        using var reader = command.ExecuteReader();
        var result = BuildTable(reader);

        connection.Close();

        return result;
    }

    private protected async Task<QueryResultTable> ExecuteQueryFromConnectionInternalAsync(DbConnection connection, ParameterizedSqlCommand query)
    {
        await using var command = CreateCommandInternal(connection, query);
        command.CommandTimeout = 45;

        await connection.OpenAsync();
        
        await using var reader = await command.ExecuteReaderAsync();
        var result = BuildTable(reader);

        await connection.CloseAsync();

        return result;
    }

    private QueryResultTable BuildTable(DbDataReader reader)
    {
        var fieldCount = reader.FieldCount;
        var columnNames = Enumerable.Range(0, fieldCount).Select(reader.GetName).ToArray();
        var result = new QueryResultTable(columnNames);

        while (reader.Read())
        {
            var row = new List<string>(fieldCount);
            for (int i = 0; i < fieldCount; i++)
            {
                var value = reader.IsDBNull(i) ? string.Empty : (reader[i].ToString() ?? string.Empty);
                row.Add(value);
            }
            result.AddRow(row.ToArray());
        }

        return result;
    }
    
    private DbCommand CreateCommandInternal(DbConnection connection, ParameterizedSqlCommand query)
    {
        var command = connection.CreateCommand();
        command.CommandText = query.Body;
        command.Parameters.AddRange(query.Parameters.ToArray());
        return command;
    }
}
