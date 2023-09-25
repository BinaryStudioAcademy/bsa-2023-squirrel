using Microsoft.AspNetCore.SignalR;
using Squirrel.ConsoleApp.Models;
using Squirrel.SqlService.BLL.Interfaces.ConsoleAppHub;

namespace Squirrel.SqlService.BLL.Hubs;

public sealed class ConsoleAppHub : Hub<IExecuteOnClientSide>
{
    private readonly IProcessReceivedDataService _processReceivedDataService;

    private readonly Dictionary<string, Func<Guid, QueryResultTable, Task>> _requestActionToProcessReceivedData = new();

    public ConsoleAppHub(IProcessReceivedDataService processReceivedDataService)
    {
        _processReceivedDataService = processReceivedDataService;
        InitRequestActionDict();
    }

    public override async Task OnConnectedAsync()
    {
        await Clients.Caller.SetClientId(Context.UserIdentifier!);
    }
    
    public async Task ProcessReceivedDataFromClientSide(Guid queryId, string requestActionName, QueryResultTable queryResultTable)
    {
        if (!_requestActionToProcessReceivedData.ContainsKey(requestActionName))
        {
            return;
        }
        
        await (_requestActionToProcessReceivedData.GetValueOrDefault(requestActionName)
            ?.Invoke(queryId, queryResultTable) ?? throw new NullReferenceException());
    }

    private void InitRequestActionDict()
    {
        _requestActionToProcessReceivedData.Add("GetAllTablesNamesAsync", _processReceivedDataService.AllTablesNamesProcessReceivedDataAsync);
        _requestActionToProcessReceivedData.Add("GetTableDataAsync", _processReceivedDataService.TableDataProcessReceivedDataAsync);
        _requestActionToProcessReceivedData.Add("GetAllStoredProceduresNamesAsync", _processReceivedDataService.AllStoredProceduresNamesProcessReceivedDataAsync);
        _requestActionToProcessReceivedData.Add("GetStoredProcedureDefinitionAsync", _processReceivedDataService.StoredProceduresWithDetailProcessReceivedDataAsync);
        _requestActionToProcessReceivedData.Add("GetAllFunctionsNamesAsync", _processReceivedDataService.AllFunctionsNamesProcessReceivedDataAsync);
        _requestActionToProcessReceivedData.Add("GetFunctionDefinitionAsync", _processReceivedDataService.FunctionDefinitionProcessReceivedDataAsync);
        _requestActionToProcessReceivedData.Add("GetAllViewsNamesAsync", _processReceivedDataService.AllViewsNamesProcessReceivedDataAsync);
        _requestActionToProcessReceivedData.Add("GetViewDefinitionAsync", _processReceivedDataService.ViewDefinitionProcessReceivedDataAsync);
        _requestActionToProcessReceivedData.Add("GetTableStructureAsync", _processReceivedDataService.TableStructureProcessReceivedDataAsync);
        _requestActionToProcessReceivedData.Add("GetTableChecksAndUniqueConstraintsAsync", _processReceivedDataService.TableChecksAndUniqueConstraintsProcessReceivedDataAsync);
        _requestActionToProcessReceivedData.Add("GetStoredProceduresWithDetailAsync", _processReceivedDataService.StoredProceduresWithDetailProcessReceivedDataAsync);
        _requestActionToProcessReceivedData.Add("GetFunctionsWithDetailAsync", _processReceivedDataService.FunctionsWithDetailProcessReceivedDataAsync);
        _requestActionToProcessReceivedData.Add("GetViewsWithDetailAsync", _processReceivedDataService.ViewsWithDetailProcessReceivedDataAsync);
        _requestActionToProcessReceivedData.Add("GetUserDefinedTypesWithDefaultsAndRulesAndDefinitionAsync", _processReceivedDataService.UserDefinedTypesWithDefaultsAndRulesAndDefinitionProcessReceivedDataAsync);
        _requestActionToProcessReceivedData.Add("GetUserDefinedTableTypesAsync", _processReceivedDataService.UserDefinedTableTypesProcessReceivedDataAsync);
        _requestActionToProcessReceivedData.Add("RemoteConnectAsync", _processReceivedDataService.RemoteConnectProcessAsync);
        _requestActionToProcessReceivedData.Add("ExecuteScriptAsync", _processReceivedDataService.ExecuteScriptProcessReceivedDataAsync);
    }
}