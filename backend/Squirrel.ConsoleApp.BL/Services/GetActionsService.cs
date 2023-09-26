using System.Data.SqlClient;
using Squirrel.ConsoleApp.BL.Interfaces;
using Squirrel.ConsoleApp.Models;

namespace Squirrel.ConsoleApp.BL.Services;

public class GetActionsService : IGetActionsService
{
    private readonly IDbQueryProvider _queryProvider;
    private readonly IDatabaseService _databaseService;

    public GetActionsService(IConnectionFileService connectionFileService, IConnectionStringService connectionStringService)
    {
        var connectionString = connectionFileService.ReadFromFile();

        _queryProvider = DatabaseServiceFactory.CreateDbQueryProvider(connectionString.DbEngine);
        _databaseService = DatabaseServiceFactory.CreateDatabaseService(connectionString.DbEngine, connectionStringService.BuildConnectionString(connectionString));
    }

    public async Task<QueryResultTable> GetAllTablesNamesAsync()
        => await _databaseService.ExecuteQueryAsync(_queryProvider.GetTablesNamesQuery());

    public async Task<QueryResultTable> GetTableDataAsync(string schema, string name, int rowsCount)
        => await _databaseService.ExecuteQueryAsync(_queryProvider.GetTableDataQuery(schema, name, rowsCount));

    public async Task<QueryResultTable> GetAllStoredProceduresNamesAsync()
        => await _databaseService.ExecuteQueryAsync(_queryProvider.GetStoredProceduresNamesQuery());

    public async Task<QueryResultTable> GetStoredProcedureDefinitionAsync(string storedProcedureSchema, string storedProcedureName)
        => await _databaseService.ExecuteQueryAsync(_queryProvider.GetStoredProcedureDefinitionQuery(storedProcedureSchema, storedProcedureName));

    public async Task<QueryResultTable> GetAllFunctionsNamesAsync()
        => await _databaseService.ExecuteQueryAsync(_queryProvider.GetFunctionsNamesQuery());

    public async Task<QueryResultTable> GetFunctionDefinitionAsync(string functionSchema, string functionName)
        => await _databaseService.ExecuteQueryAsync(_queryProvider.GetFunctionDefinitionQuery(functionSchema, functionName));

    public async Task<QueryResultTable> GetAllViewsNamesAsync()
        => await _databaseService.ExecuteQueryAsync(_queryProvider.GetViewsNamesQuery());

    public async Task<QueryResultTable> GetViewDefinitionAsync(string viewSchema, string viewName)
        => await _databaseService.ExecuteQueryAsync(_queryProvider.GetViewDefinitionQuery(viewSchema, viewName));

    public async Task<QueryResultTable> GetTableStructureAsync(string schema, string name)
        => await _databaseService.ExecuteQueryAsync(_queryProvider.GetTableStructureQuery(schema, name));

    public async Task<QueryResultTable> GetTableChecksAndUniqueConstraintsAsync(string schema, string name)
        => await _databaseService.ExecuteQueryAsync(_queryProvider.GetTableChecksAndUniqueConstraintsQuery(schema, name));

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

    public async Task<QueryResultTable> ExecuteScriptAsync(string scriptContent)
        => await _databaseService.ExecuteQueryAsync(new ParameterizedSqlCommand(scriptContent, new List<SqlParameter>()));
}
