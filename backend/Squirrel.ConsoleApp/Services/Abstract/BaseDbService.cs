using Squirrel.ConsoleApp.Exceptions;
using Squirrel.ConsoleApp.Interfaces;
using System.Data.Common;
using System.Text;

namespace Squirrel.ConsoleApp.Services.Abstract;
public abstract class BaseDbService: IDatabaseService
{
    public string ConnectionString { get; set; }

    public BaseDbService(string connectionString)
    {
        ConnectionString = connectionString;
    }

    public abstract string ExecuteQuery(string query);
    public abstract Task<string> ExecuteQueryAsync(string query);

    private protected string ExecuteQueryFromConnectionInternal(DbConnection connection, string query)
    {
        using var command = CreateCommandInternal(connection, query);
        try
        {
            connection.Open();
            using var reader = command.ExecuteReader();

            var result = BuildQueryResultString(reader);

            return result;
        }
        catch (Exception ex)
        {
            throw new DatabaseException(ex.Message);
        }
    }

    private protected async Task<string> ExecuteQueryFromConnectionInternalAsync(DbConnection connection, string query)
    {
        using var command = CreateCommandInternal(connection, query);
        try
        {
            await connection.OpenAsync();
            await using var reader = await command.ExecuteReaderAsync();

            var result = BuildQueryResultString(reader);

            return result;
        }
        catch (Exception ex)
        {
            throw new DatabaseException(ex.Message);
        }
    }

    private string BuildQueryResultString(DbDataReader reader)
    {
        var stringBuilder = new StringBuilder();
        while (reader.Read())
        {
            for (int i = 0; i < reader.FieldCount; i++)
            {
                stringBuilder.Append(reader[i] + "\t");
            }
            stringBuilder.AppendLine();
        }

        return stringBuilder.ToString();
    }

    private DbCommand CreateCommandInternal(DbConnection connection, string query)
    {
        var command = connection.CreateCommand();
        command.CommandText = query;
        return command;
    }
}
