using Squirrel.ConsoleApp.Models;

namespace Squirrel.ConsoleApp.BL.Interfaces;

public interface IGetActionsService
{
    Task<QueryResultTable> GetAllTablesNamesAsync();

    Task<QueryResultTable> GetAllStoredProceduresNamesAsync();
    Task<QueryResultTable> GetStoredProcedureDefinitionAsync(string storedProcedureSchema, string storedProcedureName);

    Task<QueryResultTable> GetAllFunctionsNamesAsync();
    Task<QueryResultTable> GetFunctionDefinitionAsync(string functionSchema, string functionName);

    Task<QueryResultTable> GetAllViewsNamesAsync();
    Task<QueryResultTable> GetViewDefinitionAsync(string viewSchema, string viewName);

    Task<QueryResultTable> GetTableStructureAsync(string schema, string name);
    Task<QueryResultTable> GetTableChecksAndUniqueConstraintsAsync(string schema, string name);

    Task<QueryResultTable> GetStoredProceduresWithDetailAsync();
    Task<QueryResultTable> GetFunctionsWithDetailAsync();
    Task<QueryResultTable> GetViewsWithDetailAsync();

    Task<QueryResultTable> GetUserDefinedTypesWithDefaultsAndRulesAndDefinitionAsync();
    Task<QueryResultTable> GetUserDefinedTableTypesAsync();
    Task<QueryResultTable> ExecuteScriptAsync(string scriptContent);
}