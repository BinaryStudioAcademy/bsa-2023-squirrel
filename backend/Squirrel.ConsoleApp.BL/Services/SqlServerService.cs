﻿using System.Data.SqlClient;
using Squirrel.ConsoleApp.BL.Services.Abstract;
using Squirrel.ConsoleApp.Models;

namespace Squirrel.ConsoleApp.BL.Services;

public class SqlServerService : BaseDbService
{
    public SqlServerService(string connectionString) : base(connectionString)
    {
    }

    public override QueryResultTable ExecuteQuery(ParameterizedSqlCommand query)
    {
        using var connection = new SqlConnection(ConnectionString);
        return ExecuteQueryFromConnectionInternal(connection, query);
    }

    public override async Task<QueryResultTable> ExecuteQueryAsync(ParameterizedSqlCommand query)
    {
        await using var connection = new SqlConnection(ConnectionString);
        return await ExecuteQueryFromConnectionInternalAsync(connection, query);
    }
}
