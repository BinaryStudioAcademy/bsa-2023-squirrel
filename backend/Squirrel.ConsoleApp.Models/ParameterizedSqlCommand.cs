using System.Data.SqlClient;

namespace Squirrel.ConsoleApp.Models;

public record ParameterizedSqlCommand(string Body, ICollection<SqlParameter> Parameters);