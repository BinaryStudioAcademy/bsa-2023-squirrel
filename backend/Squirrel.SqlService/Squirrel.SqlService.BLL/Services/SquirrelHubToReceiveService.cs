using Squirrel.Core.BLL.Interfaces;
using Squirrel.SqlService.BLL.Models.SquirrelHub;

namespace Squirrel.SqlService.BLL.Services;

public class SquirrelHubToReceiveService : ISquirrelHubToReceive
{
    // TODO: Implement all functions to process data received from ConsoleApp 

    /// <summary>
    /// Just for debugging and demo
    /// </summary>
    private async Task ReceiveExecutedQueryShowResultAsync(string clientId, QueryResultTableDTO queryResultTableDTO)
    {
        Console.WriteLine($"------------------------------------------------------------------");
        Console.WriteLine($"Successfully recived data from user '{clientId}'");
        Console.WriteLine($"    result:");
        Console.WriteLine(queryResultTableDTO);
        Console.WriteLine($"------------------------------------------------------------------");
    }

    public async Task ReceiveAllFunctionsNamesAsync(string clientId, QueryResultTableDTO queryResultTableDTO)
    {
        await ReceiveExecutedQueryShowResultAsync(clientId, queryResultTableDTO);
    }

    public async Task ReceiveAllStoredProceduresNamesAsync(string clientId, QueryResultTableDTO queryResultTableDTO)
    {
        await ReceiveExecutedQueryShowResultAsync(clientId, queryResultTableDTO);
    }

    public async Task ReceiveAllTablesNamesAsync(string clientId, QueryResultTableDTO queryResultTableDTO)
    {
        await ReceiveExecutedQueryShowResultAsync(clientId, queryResultTableDTO);
    }

    public async Task ReceiveAllViewsNamesAsync(string clientId, QueryResultTableDTO queryResultTableDTO)
    {
        await ReceiveExecutedQueryShowResultAsync(clientId, queryResultTableDTO);
    }

    public async Task ReceiveDbTablesCheckAndUniqueConstraintsAsync(string clientId, QueryResultTableDTO queryResultTableDTO)
    {
        await ReceiveExecutedQueryShowResultAsync(clientId, queryResultTableDTO);
    }

    public async Task ReceiveDbTablesStructureAsync(string clientId, QueryResultTableDTO queryResultTableDTO)
    {
        await ReceiveExecutedQueryShowResultAsync(clientId, queryResultTableDTO);
    }

    public async Task ReceiveFunctionDefinitionAsync(string clientId, QueryResultTableDTO queryResultTableDTO)
    {
        await ReceiveExecutedQueryShowResultAsync(clientId, queryResultTableDTO);
    }

    public async Task ReceiveFunctionsWithDetailAsync(string clientId, QueryResultTableDTO queryResultTableDTO)
    {
        await ReceiveExecutedQueryShowResultAsync(clientId, queryResultTableDTO);
    }

    public async Task ReceiveStoredProcedureDefinitionAsync(string clientId, QueryResultTableDTO queryResultTableDTO)
    {
        await ReceiveExecutedQueryShowResultAsync(clientId, queryResultTableDTO);
    }

    public async Task ReceiveStoredProceduresWithDetailAsync(string clientId, QueryResultTableDTO queryResultTableDTO)
    {
        await ReceiveExecutedQueryShowResultAsync(clientId, queryResultTableDTO);
    }

    public async Task ReceiveTableDataAsync(string clientId, QueryResultTableDTO queryResultTableDTO)
    {
        await ReceiveExecutedQueryShowResultAsync(clientId, queryResultTableDTO);
    }

    public async Task ReceiveUserDefinedTableTypesAsync(string clientId, QueryResultTableDTO queryResultTableDTO)
    {
        await ReceiveExecutedQueryShowResultAsync(clientId, queryResultTableDTO);
    }

    public async Task ReceiveUserDefinedTypesWithDefaultsAndRulesAndDefinitionAsync(string clientId, QueryResultTableDTO queryResultTableDTO)
    {
        await ReceiveExecutedQueryShowResultAsync(clientId, queryResultTableDTO);
    }

    public async Task ReceiveViewDefinitionAsync(string clientId, QueryResultTableDTO queryResultTableDTO)
    {
        await ReceiveExecutedQueryShowResultAsync(clientId, queryResultTableDTO);
    }

    public async Task ReceiveViewsWithDetailAsync(string clientId, QueryResultTableDTO queryResultTableDTO)
    {
        await ReceiveExecutedQueryShowResultAsync(clientId, queryResultTableDTO);
    }
}
