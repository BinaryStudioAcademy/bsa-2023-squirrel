using Squirrel.ConsoleApp.Interfaces;
using Squirrel.ConsoleApp.Models;
using Squirrel.ConsoleApp.Providers;
using System.Data;
using DbType = Squirrel.ConsoleApp.Models.DbType;

namespace Squirrel.ConsoleApp.Services
{
    public class GetActionsService : IGetActionsService
    {
        private readonly DbType _dbType;
        private readonly IDatabaseService _databaseService;

        public GetActionsService(DbType dbType, string connection)
        {
            _dbType = dbType;
            _databaseService = DatabaseFactory.CreateDatabaseService(dbType, connection);
        }

        public async Task<UserAction> GetAllFunctionsAsync()
            => await GetActionsAsync(DataType.Functions, DatabaseFactory.GetFunctionsQuery(_dbType));

        public async Task<UserAction> GetAllStoredProceduresAsync()
            => await GetActionsAsync(DataType.StoredProcedures, DatabaseFactory.GetStoredProceduresQuery(_dbType));

        public async Task<UserAction> GetAllTablesAsync()
            => await GetActionsAsync(DataType.Tables, DatabaseFactory.GetTablesQuery(_dbType));

        public async Task<TableData> GetTableDataAsync(string tableName, int rowsCount)
        {
            return await GetTableDataInternalAsync($"Data from '{tableName}' Table for first '{rowsCount}' rows",
                                            DataType.TableData,
                                            DatabaseFactory.GetTableDataQuery(_dbType, tableName, rowsCount));
        }

        public async Task<UserAction> GetFunctionAsync(string functionName)
        {
            return await GetUserActionAsync($"Data from '{functionName}' Function",
                                            DataType.FunctionData,
                                            DatabaseFactory.GetFunctionQuery(_dbType, functionName));
        }

        public async Task<UserAction> GetStoredProcedureAsync(string storedProcedureName)
        {
            return await GetUserActionAsync($"Data from '{storedProcedureName}' StoredProcedure",
                                            DataType.StoredProcedureData,
                                            DatabaseFactory.GetStoredProcedureQuery(_dbType, storedProcedureName));
        }

        private async Task<UserAction> GetActionsAsync(DataType dataType, string query)
        {
            var result = await _databaseService.ExecuteQueryAsync(query);
            var data = result.Rows.Select(ConvertRowToString).ToList();

            return new UserAction
            {
                Name = dataType.ToString(),
                Type = dataType,
                Data = data
            };
        }

        private async Task<UserAction> GetUserActionAsync(string name, DataType dataType, string query)
        {
            var result = await _databaseService.ExecuteQueryAsync(query);
            var data = result.Rows.Select(ConvertRowToString).ToList();

            return new UserAction
            {
                Name = name,
                Type = dataType,
                Data = data
            };
        }

        private async Task<TableData> GetTableDataInternalAsync(string name, DataType dataType, string query)
        {
            var result = await _databaseService.ExecuteQueryAsync(query);

            return new TableData
            {
                Name = name,
                Type = dataType,
                Table = result
            };
        }

        private string ConvertRowToString(string[] row) => string.Join(", ", row);
    }
}
