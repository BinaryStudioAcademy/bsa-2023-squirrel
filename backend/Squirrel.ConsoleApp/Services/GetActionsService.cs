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

        public async Task<QueryResultTable> GetAllTablesNamesAsync() 
            => await _databaseService.ExecuteQueryAsync(_queryProvider.GetTablesNamesQuery());

        public async Task<QueryResultTable> GetTableDataAsync(string tableName, int rowsCount) 
            => await _databaseService.ExecuteQueryAsync(_queryProvider.GetTableDataQuery(tableName, rowsCount));

        public async Task<QueryResultTable> GetAllStoredProceduresNamesAsync() 
            => await _databaseService.ExecuteQueryAsync(_queryProvider.GetStoredProceduresNamesQuery());

        public async Task<QueryResultTable> GetStoredProcedureDefinitionAsync(string storedProcedureName) 
            => await _databaseService.ExecuteQueryAsync(_queryProvider.GetStoredProcedureDefinitionQuery(storedProcedureName));

        public async Task<QueryResultTable> GetAllFunctionsNamesAsync() 
            => await _databaseService.ExecuteQueryAsync(_queryProvider.GetFunctionsNamesQuery());

        public async Task<QueryResultTable> GetFunctionDefinitionAsync(string functionName) 
            => await _databaseService.ExecuteQueryAsync(_queryProvider.GetFunctionDefinitionQuery(functionName));

        public async Task<QueryResultTable> GetAllViewsNamesAsync() 
            => await _databaseService.ExecuteQueryAsync(_queryProvider.GetViewsNamesQuery());

        public async Task<QueryResultTable> GetViewDefinitionAsync(string viewName) 
            => await _databaseService.ExecuteQueryAsync(_queryProvider.GetViewDefinitionQuery(viewName));

        public async Task<QueryResultTable> GetDbTablesStructureAsync() 
            => await _databaseService.ExecuteQueryAsync(_queryProvider.GetTablesStructureQuery());

        public async Task<QueryResultTable> GetDbTablesCheckAndUniqueConstraintsAsync() 
            => await _databaseService.ExecuteQueryAsync(_queryProvider.GetTablesCheckAndUniqueConstraintsQuery());

        public async Task<QueryResultTable> GetStoredProceduresWithDetailAsync() 
            => await _databaseService.ExecuteQueryAsync(_queryProvider.GetStoredProceduresWithDetailsQuery());

        public async Task<QueryResultTable> GetFunctionsWithDetailAsync() 
            => await _databaseService.ExecuteQueryAsync(_queryProvider.GetFunctionsWithDetailsQuery());

        public async Task<QueryResultTable> GetViewsWithDetailAsync() 
            => await _databaseService.ExecuteQueryAsync(_queryProvider.GetViewsWithDetailsQuery());

        public async Task<QueryResultTable> GetUserDefinedTypesWithDefaultsAndRulesAndDefinitionAsync() 
            => await _databaseService.ExecuteQueryAsync(_queryProvider.GetUserDefinedTypesWithDefaultsAndRulesAndDefinitionQuery());

        public async Task<QueryResultTable> GetUserDefinedTableTypesAsync() 
            => await _databaseService.ExecuteQueryAsync(_queryProvider.GetUserDefinedTableTypesStructureQuery());
    }
}
