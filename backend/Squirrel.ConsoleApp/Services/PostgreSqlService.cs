using Npgsql;
using Squirrel.ConsoleApp.Exceptions;
using Squirrel.ConsoleApp.Interfaces;
using System.Data.SqlClient;
using System.Text;

namespace Squirrel.ConsoleApp.Services
{
    public class PostgreSqlService : IDatabaseService
    {
        private string _connectionString;

        public string ConnectionString
        {
            get { return _connectionString; }
            set { _connectionString = value; }
        }

        public PostgreSqlService(string connectionString)
        {
            _connectionString = connectionString;
        }

        public async Task<string> ExecuteQueryAsync(string query)
        {
            using NpgsqlConnection connection = new NpgsqlConnection(_connectionString);

            using NpgsqlCommand command = connection.CreateCommand();
            command.CommandText = query;
            try
            {
                await connection.OpenAsync();
                NpgsqlDataReader reader = await command.ExecuteReaderAsync();
                
                var result = BuildQueryResultString(reader);
                await reader.CloseAsync();

                return result;
            }
            catch (Exception ex)
            {
                throw new DatabaseException(ex.Message);
            }
        }

        public string ExecuteQuery(string query)
        {
            using NpgsqlConnection connection = new NpgsqlConnection(_connectionString);

            using NpgsqlCommand command = connection.CreateCommand();
            command.CommandText = query;
            try
            {
                connection.Open();
                NpgsqlDataReader reader = command.ExecuteReader();

                var result = BuildQueryResultString(reader);
                reader.Close();

                return result;
            }
            catch (Exception ex)
            {
                throw new DatabaseException(ex.Message);
            }
        }

        private string BuildQueryResultString(NpgsqlDataReader reader)
        {
            var stringBuilder = new StringBuilder();
            while (reader.Read())
            {
                for (int i = 0; i < reader.FieldCount; i++)
                {
                    stringBuilder.AppendLine(reader[i] + "\t");
                }
            }

            return stringBuilder.ToString();
        }
    }
}
