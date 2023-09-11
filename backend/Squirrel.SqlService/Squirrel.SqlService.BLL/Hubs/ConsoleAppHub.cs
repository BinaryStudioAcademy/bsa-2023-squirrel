using Microsoft.AspNetCore.SignalR;
using Squirrel.SqlService.BLL.Interfaces.ConsoleAppHub;
using Squirrel.SqlService.BLL.Models.ConsoleAppHub;

namespace Squirrel.Core.BLL.Hubs;

public sealed class ConsoleAppHub : Hub<IExecuteOnClientSide>
{
    private readonly IProcessReceivedDataService _processReceivedDataService;
    private readonly Dictionary<string, Func<string, QueryResultTableDTO, Task>> requestActionToProcessReceivedData = new();

    public ConsoleAppHub(IProcessReceivedDataService processReceivedDataService)
    {
        _processReceivedDataService = processReceivedDataService;
        InitRequestActionDict();
    }

    public override async Task OnConnectedAsync()
    {
        await Clients.Caller.SetClientId(Context.UserIdentifier);
    }

    public async Task ProcessReceivedDataFromClientSide(string clientId, string requestActionName, QueryResultTableDTO queryResultTableDTO)
    {
        if (!requestActionToProcessReceivedData.ContainsKey(requestActionName))
        {
            return;
        }

        await (requestActionToProcessReceivedData.GetValueOrDefault(requestActionName)
            ?.Invoke(clientId, queryResultTableDTO) ?? throw new NullReferenceException());
    }

    private void InitRequestActionDict()
    {
        requestActionToProcessReceivedData.Add("GetAllTablesNamesAsync", _processReceivedDataService.AllTablesNamesProcessReceivedDataAsync);
        requestActionToProcessReceivedData.Add("GetTableDataAsync", _processReceivedDataService.TableDataProcessReceivedDataAsync);
        requestActionToProcessReceivedData.Add("GetAllStoredProceduresNamesAsync", _processReceivedDataService.AllStoredProceduresNamesProcessReceivedDataAsync);
        requestActionToProcessReceivedData.Add("GetStoredProcedureDefinitionAsync", _processReceivedDataService.StoredProcedureDefinitionProcessReceivedDataAsync);
        requestActionToProcessReceivedData.Add("GetAllFunctionsNamesAsync", _processReceivedDataService.AllFunctionsNamesProcessReceivedDataAsync);
        requestActionToProcessReceivedData.Add("GetFunctionDefinitionAsync", _processReceivedDataService.FunctionDefinitionProcessReceivedDataAsync);
        requestActionToProcessReceivedData.Add("GetAllViewsNamesAsync", _processReceivedDataService.AllViewsNamesProcessReceivedDataAsync);
        requestActionToProcessReceivedData.Add("GetViewDefinitionAsync", _processReceivedDataService.ViewDefinitionProcessReceivedDataAsync);
        requestActionToProcessReceivedData.Add("GetDbTablesStructureAsync", _processReceivedDataService.DbTablesStructureProcessReceivedDataAsync);
        requestActionToProcessReceivedData.Add("GetDbTablesCheckAndUniqueConstraintsAsync", _processReceivedDataService.DbTablesCheckAndUniqueConstraintsProcessReceivedDataAsync);
        requestActionToProcessReceivedData.Add("GetStoredProceduresWithDetailAsync", _processReceivedDataService.StoredProceduresWithDetailProcessReceivedDataAsync);
        requestActionToProcessReceivedData.Add("GetFunctionsWithDetailAsync", _processReceivedDataService.FunctionsWithDetailProcessReceivedDataAsync);
        requestActionToProcessReceivedData.Add("GetViewsWithDetailAsync", _processReceivedDataService.ViewsWithDetailProcessReceivedDataAsync);
        requestActionToProcessReceivedData.Add("GetUserDefinedTypesWithDefaultsAndRulesAndDefinitionAsync", _processReceivedDataService.UserDefinedTypesWithDefaultsAndRulesAndDefinitionProcessReceivedDataAsync);
        requestActionToProcessReceivedData.Add("GetUserDefinedTableTypesAsync", _processReceivedDataService.UserDefinedTableTypesProcessReceivedDataAsync);
    }
}