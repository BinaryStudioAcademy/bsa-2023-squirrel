using Squirrel.ConsoleApp.Interfaces;
using Squirrel.ConsoleApp.Models;
using Squirrel.ConsoleApp.Providers;

namespace Squirrel.ConsoleApp.Services
{
    public class GetActionsService : IGetActionsService
    {
        private readonly IDbQueryProvider _queryProvider;
        private readonly IDatabaseService _databaseService;

        public GetActionsService(DbEngine dbType, IDbQueryProvider queryProvider, string connection)
        {
            _queryProvider = queryProvider;
            _databaseService = DatabaseFactory.CreateDatabaseService(dbType, connection);
        }

        public async Task<QueryResultTable> GetAllTablesAsync()
        {
            var query = _queryProvider.GetTablesQuery();
            return await _databaseService.ExecuteQueryAsync(query);
        }

        public async Task<QueryResultTable> GetTableDataAsync(string tableName, int rowsCount)
        {
            var query = _queryProvider.GetTableDataQuery(tableName, rowsCount);
            return await _databaseService.ExecuteQueryAsync(query);
        }

        public async Task<QueryResultTable> GetAllStoredProceduresAsync()
        {
            var query = _queryProvider.GetStoredProceduresQuery();
            return await _databaseService.ExecuteQueryAsync(query);
        }

        public async Task<QueryResultTable> GetStoredProcedureDefinitionAsync(string storedProcedureName)
        {
            var query = _queryProvider.GetStoredProcedureDefinitionQuery(storedProcedureName);
            return await _databaseService.ExecuteQueryAsync(query);
        }

        public async Task<QueryResultTable> GetAllFunctionsAsync()
        {
            var query = _queryProvider.GetFunctionsQuery();
            return await _databaseService.ExecuteQueryAsync(query);
        }

        public async Task<QueryResultTable> GetFunctionDefinitionAsync(string functionName)
        {
            var query = _queryProvider.GetFunctionDefinitionQuery(functionName);
            return await _databaseService.ExecuteQueryAsync(query);
        }
    }
}
