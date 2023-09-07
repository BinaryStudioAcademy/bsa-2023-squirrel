using Microsoft.AspNetCore.SignalR;
using Squirrel.Core.BLL.Interfaces;
using Squirrel.SqlService.BLL.Models.SquirrelHub;

namespace Squirrel.Core.BLL.Hubs;

public sealed class SquirrelHub : Hub<ISquirrelHubToSend>, ISquirrelHubToReceive
{
    private ISquirrelHubToReceive _hubToReceiveService;

    public SquirrelHub(ISquirrelHubToReceive hubToReceiveService)
    {
        _hubToReceiveService = hubToReceiveService;
    }

    public override async Task OnConnectedAsync()
    {
        await Clients.Caller.SetClientId(Context.UserIdentifier);
    }

    public async Task ReceiveAllFunctionsNamesAsync(string clientId, QueryResultTableDTO queryResultTableDTO)
        => await _hubToReceiveService.ReceiveAllFunctionsNamesAsync(clientId, queryResultTableDTO);

    public async Task ReceiveAllStoredProceduresNamesAsync(string clientId, QueryResultTableDTO queryResultTableDTO)
        => await _hubToReceiveService.ReceiveAllStoredProceduresNamesAsync(clientId, queryResultTableDTO);

    public async Task ReceiveAllTablesNamesAsync(string clientId, QueryResultTableDTO queryResultTableDTO)
       => await _hubToReceiveService.ReceiveAllTablesNamesAsync(clientId, queryResultTableDTO);

    public async Task ReceiveAllViewsNamesAsync(string clientId, QueryResultTableDTO queryResultTableDTO)
        => await _hubToReceiveService.ReceiveAllViewsNamesAsync(clientId, queryResultTableDTO);

    public async Task ReceiveDbTablesCheckAndUniqueConstraintsAsync(string clientId, QueryResultTableDTO queryResultTableDTO)
        => await _hubToReceiveService.ReceiveDbTablesCheckAndUniqueConstraintsAsync(clientId, queryResultTableDTO);

    public async Task ReceiveDbTablesStructureAsync(string clientId, QueryResultTableDTO queryResultTableDTO)
        => await _hubToReceiveService.ReceiveDbTablesStructureAsync(clientId, queryResultTableDTO);

    public async Task ReceiveFunctionDefinitionAsync(string clientId, QueryResultTableDTO queryResultTableDTO)
        => await _hubToReceiveService.ReceiveFunctionDefinitionAsync(clientId, queryResultTableDTO);

    public async Task ReceiveFunctionsWithDetailAsync(string clientId, QueryResultTableDTO queryResultTableDTO)
        => await _hubToReceiveService.ReceiveFunctionsWithDetailAsync(clientId, queryResultTableDTO);

    public async Task ReceiveStoredProcedureDefinitionAsync(string clientId, QueryResultTableDTO queryResultTableDTO)
        => await _hubToReceiveService.ReceiveStoredProcedureDefinitionAsync(clientId, queryResultTableDTO);

    public async Task ReceiveStoredProceduresWithDetailAsync(string clientId, QueryResultTableDTO queryResultTableDTO)
        => await _hubToReceiveService.ReceiveStoredProceduresWithDetailAsync(clientId, queryResultTableDTO);

    public async Task ReceiveTableDataAsync(string clientId, QueryResultTableDTO queryResultTableDTO)
        => await _hubToReceiveService.ReceiveTableDataAsync(clientId, queryResultTableDTO);

    public async Task ReceiveUserDefinedTableTypesAsync(string clientId, QueryResultTableDTO queryResultTableDTO)
        => await _hubToReceiveService.ReceiveUserDefinedTableTypesAsync(clientId, queryResultTableDTO);

    public async Task ReceiveUserDefinedTypesWithDefaultsAndRulesAndDefinitionAsync(string clientId, QueryResultTableDTO queryResultTableDTO)
        => await _hubToReceiveService.ReceiveUserDefinedTypesWithDefaultsAndRulesAndDefinitionAsync(clientId, queryResultTableDTO);

    public async Task ReceiveViewDefinitionAsync(string clientId, QueryResultTableDTO queryResultTableDTO)
        => await _hubToReceiveService.ReceiveViewDefinitionAsync(clientId, queryResultTableDTO);

    public async Task ReceiveViewsWithDetailAsync(string clientId, QueryResultTableDTO queryResultTableDTO)
        => await _hubToReceiveService.ReceiveViewsWithDetailAsync(clientId, queryResultTableDTO);
}