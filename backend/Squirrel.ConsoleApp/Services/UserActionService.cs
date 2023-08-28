using Squirrel.ConsoleApp.Interfaces;
using Squirrel.ConsoleApp.Models;
using System.Data;
using DbType = Squirrel.ConsoleApp.Models.DbType;

namespace Squirrel.ConsoleApp.Services
{
    public class UserActionService : IGetActionsService
    {
        private readonly DbType _dbType;
        private readonly IDatabaseService _databaseService;

        public UserActionService(DbType dbType, string connection)
        {
            _dbType = dbType;
            _databaseService = DatabaseFactory.CreateDatabaseService(dbType, connection);
        }

        public async Task<IEnumerable<UserAction>> GetAllFunctionsAsync()
            => await GetActionsAsync(DataType.Function, DatabaseFactory.GetFunctionsQuery(_dbType));

        public async Task<IEnumerable<UserAction>> GetAllStoredProceduresAsync()
            => await GetActionsAsync(DataType.StoredProcedure, DatabaseFactory.GetStoredProceduresQuery(_dbType));

        public async Task<IEnumerable<UserAction>> GetAllTablesAsync()
            => await GetActionsAsync(DataType.Table, DatabaseFactory.GetTablesQuery(_dbType));

        public async Task<UserAction> GetTableDataAsync(string tableName, int rowsCount)
        {
            var query = DatabaseFactory.GetTableDataQuery(_dbType, tableName, rowsCount);
            var result = await _databaseService.ExecuteQueryAsync(query);

            var data = string.Join(Environment.NewLine, result.Rows.Select(ConvertRowToString));

            return new UserAction
            {
                Name = $"Data from '{tableName}' for first '{rowsCount}' rows",
                Type = DataType.TableData,
                Data = data
            };
        }

        public async Task<UserAction> GetFunctionAsync(string functionName)
        {
            var query = DatabaseFactory.GetFunctionQuery(_dbType, functionName);
            var result = await _databaseService.ExecuteQueryAsync(query);

            var data = string.Join(Environment.NewLine, result.Rows.Select(ConvertRowToString));

            return new UserAction
            {
                Name = $"Data from '{functionName}' Function",
                Type = DataType.Function,
                Data = data
            };
        }

        public async Task<UserAction> GetStoredProcedureAsync(string storedProcedureName)
        {
            var query = DatabaseFactory.GetStoredProcedureQuery(_dbType, storedProcedureName);
            var result = await _databaseService.ExecuteQueryAsync(query);

            var data = string.Join(Environment.NewLine, result.Rows.Select(ConvertRowToString));

            return new UserAction
            {
                Name = $"Data from '{storedProcedureName}' StoredProcedure",
                Type = DataType.StoredProcedure,
                Data = data
            };
        }

        private async Task<IEnumerable<UserAction>> GetActionsAsync(DataType dataType, string query)
        {
            var result = await _databaseService.ExecuteQueryAsync(query);
            return result.Rows.Select(row => new UserAction
            {
                Name = row[0],
                Type = dataType,
                Data = ConvertRowToString(row)
            });
        }

        private string ConvertRowToString(string[] row) => string.Join(", ", row);
    }
}
