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
            return await GetUserActionAsync($"Data from '{tableName}' Table for first '{rowsCount}' rows",
                                            DataType.TableData,
                                            DatabaseFactory.GetTableDataQuery(_dbType, tableName, rowsCount));
        }

        public async Task<UserAction> GetFunctionAsync(string functionName)
        {
            return await GetUserActionAsync($"Data from '{functionName}' Function",
                                            DataType.Function,
                                            DatabaseFactory.GetFunctionQuery(_dbType, functionName));
        }

        public async Task<UserAction> GetStoredProcedureAsync(string storedProcedureName)
        {
            return await GetUserActionAsync($"Data from '{storedProcedureName}' StoredProcedure",
                                            DataType.StoredProcedure,
                                            DatabaseFactory.GetStoredProcedureQuery(_dbType, storedProcedureName));
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

        private async Task<UserAction> GetUserActionAsync(string name, DataType dataType, string query)
        {
            var result = await _databaseService.ExecuteQueryAsync(query);
            return new UserAction
            {
                Name = name,
                Type = dataType,
                Data = string.Join(Environment.NewLine, result.Rows.Select(ConvertRowToString))
            };
        }

        private string ConvertRowToString(string[] row) => string.Join(", ", row);
    }
}
