using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.SignalR.Client;
using Microsoft.Extensions.DependencyInjection;
using Squirrel.ConsoleApp.BL.Interfaces;

namespace Squirrel.Core.WebAPI.Extensions;

public static class HubConnectionExtensions
{
    private static bool CurrentClientIsTarget(string currentClientId, string clientId)
    {
        if (string.IsNullOrEmpty(currentClientId))
        {
            return false;
        }

        if (!clientId.Equals(currentClientId))
        {
            return false;
        }

        return true;
    }

    public static void RegisterActions(this HubConnection hubConnection, string currentClientId, IApplicationBuilder app)
    {
        var getActionsService = app.ApplicationServices.GetRequiredService<IGetActionsService>();

        hubConnection.On("TestExecuteQueryAsync", (string clientId, string filterName, int filterRowsCount) =>
        {
            if (!CurrentClientIsTarget(currentClientId, clientId))
            {
                return;
            }

            hubConnection.InvokeAsync("TestReceiveExecutedQueryAsync", clientId, getActionsService.TestExecuteQueryAsync(filterName, filterRowsCount).Result);
        });

        hubConnection.On("GetAllTablesNamesAsync", (string clientId) =>
        {
            if (!CurrentClientIsTarget(currentClientId, clientId))
            {
                return;
            }

            hubConnection.InvokeAsync("ReceiveAllTablesNamesAsync", clientId, getActionsService.GetAllTablesNamesAsync().Result);
        });

        hubConnection.On("GetTableDataAsync", (string clientId, string tableName, int rowsCount) =>
        {
            if (!CurrentClientIsTarget(currentClientId, clientId))
            {
                return;
            }

            hubConnection.InvokeAsync("ReceiveTableDataAsync", clientId, getActionsService.GetTableDataAsync(tableName, rowsCount).Result);
        });

        hubConnection.On("GetAllStoredProceduresNamesAsync", (string clientId) =>
        {
            if (!CurrentClientIsTarget(currentClientId, clientId))
            {
                return;
            }

            hubConnection.InvokeAsync("ReceiveAllStoredProceduresNamesAsync", clientId, getActionsService.GetAllStoredProceduresNamesAsync().Result);
        });

        hubConnection.On("GetStoredProcedureDefinitionAsync", (string clientId, string storedProcedureName) =>
        {
            if (!CurrentClientIsTarget(currentClientId, clientId))
            {
                return;
            }

            hubConnection.InvokeAsync("ReceiveStoredProcedureDefinitionAsync", clientId, getActionsService.GetStoredProcedureDefinitionAsync(storedProcedureName).Result);
        });

        hubConnection.On("GetAllFunctionsNamesAsync", (string clientId) =>
        {
            if (!CurrentClientIsTarget(currentClientId, clientId))
            {
                return;
            }

            hubConnection.InvokeAsync("ReceiveAllFunctionsNamesAsync", clientId, getActionsService.GetAllFunctionsNamesAsync().Result);
        });

        hubConnection.On("GetFunctionDefinitionAsync", (string clientId, string functionName) =>
        {
            if (!CurrentClientIsTarget(currentClientId, clientId))
            {
                return;
            }

            hubConnection.InvokeAsync("ReceiveFunctionDefinitionAsync", clientId, getActionsService.GetFunctionDefinitionAsync(functionName).Result);
        });

        hubConnection.On("GetAllViewsNamesAsync", (string clientId) =>
        {
            if (!CurrentClientIsTarget(currentClientId, clientId))
            {
                return;
            }

            hubConnection.InvokeAsync("ReceiveAllViewsNamesAsync", clientId, getActionsService.GetAllViewsNamesAsync().Result);
        });

        hubConnection.On("GetViewDefinitionAsync", (string clientId, string viewName) =>
        {
            if (!CurrentClientIsTarget(currentClientId, clientId))
            {
                return;
            }

            hubConnection.InvokeAsync("ReceiveViewDefinitionAsync", clientId, getActionsService.GetViewDefinitionAsync(viewName).Result);
        });

        hubConnection.On("GetDbTablesStructureAsync", (string clientId) =>
        {
            if (!CurrentClientIsTarget(currentClientId, clientId))
            {
                return;
            }

            hubConnection.InvokeAsync("ReceiveDbTablesStructureAsync", clientId, getActionsService.GetDbTablesStructureAsync().Result);
        });

        hubConnection.On("GetDbTablesCheckAndUniqueConstraintsAsync", (string clientId) =>
        {
            if (!CurrentClientIsTarget(currentClientId, clientId))
            {
                return;
            }

            hubConnection.InvokeAsync("ReceiveDbTablesCheckAndUniqueConstraintsAsync", clientId, getActionsService.GetDbTablesCheckAndUniqueConstraintsAsync().Result);
        });

        hubConnection.On("GetStoredProceduresWithDetailAsync", (string clientId) =>
        {
            if (!CurrentClientIsTarget(currentClientId, clientId))
            {
                return;
            }

            hubConnection.InvokeAsync("ReceiveStoredProceduresWithDetailAsync", clientId, getActionsService.GetStoredProceduresWithDetailAsync().Result);
        });

        hubConnection.On("GetFunctionsWithDetailAsync", (string clientId) =>
        {
            if (!CurrentClientIsTarget(currentClientId, clientId))
            {
                return;
            }

            hubConnection.InvokeAsync("ReceiveFunctionsWithDetailAsync", clientId, getActionsService.GetFunctionsWithDetailAsync().Result);
        });

        hubConnection.On("GetViewsWithDetailAsync", (string clientId) =>
         {
             if (!CurrentClientIsTarget(currentClientId, clientId))
             {
                 return;
             }

             hubConnection.InvokeAsync("ReceiveViewsWithDetailAsync", clientId, getActionsService.GetViewsWithDetailAsync().Result);
        });

        hubConnection.On("GetUserDefinedTypesWithDefaultsAndRulesAndDefinitionAsync", (string clientId) =>
        {
            if (!CurrentClientIsTarget(currentClientId, clientId))
            {
                return;
            }

            hubConnection.InvokeAsync("ReceiveUserDefinedTypesWithDefaultsAndRulesAndDefinitionAsync", clientId, getActionsService.GetUserDefinedTypesWithDefaultsAndRulesAndDefinitionAsync().Result);
        });

        hubConnection.On("GetUserDefinedTableTypesAsync", (string clientId) =>
        {
            if (!CurrentClientIsTarget(currentClientId, clientId))
            {
                return;
            }

            hubConnection.InvokeAsync("ReceiveUserDefinedTableTypesAsync", clientId, getActionsService.GetUserDefinedTableTypesAsync().Result);
        });
    }
}