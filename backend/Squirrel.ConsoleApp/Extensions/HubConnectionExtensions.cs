using Microsoft.AspNetCore.SignalR.Client;
using Squirrel.ConsoleApp.BL.Interfaces;
using Squirrel.ConsoleApp.Models;
using Squirrel.ConsoleApp.Services;
using System.Threading.Channels;

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

    public static void RegisterActions(this HubConnection hubConnection, string currentClientId, IGetActionsService getActionsService)
    {
        hubConnection.On("GetAllTablesNamesAsync", (string clientId) =>
        {
            if (!CurrentClientIsTarget(currentClientId, clientId))
            {
                return;
            }

            hubConnection.InvokeAsync("ReceiveAllTablesNamesAsync", clientId, getActionsService.GetAllTablesNamesAsync());
        });

        hubConnection.On("GetTableDataAsync", (string clientId, string tableName, int rowsCount) =>
        {
            if (!CurrentClientIsTarget(currentClientId, clientId))
            {
                return;
            }

            hubConnection.InvokeAsync("ReceiveTableDataAsync", clientId, getActionsService.GetTableDataAsync(tableName, rowsCount));
        });

        hubConnection.On("GetAllStoredProceduresNamesAsync", (string clientId) =>
        {
            if (!CurrentClientIsTarget(currentClientId, clientId))
            {
                return;
            }

            hubConnection.InvokeAsync("ReceiveAllStoredProceduresNamesAsync", clientId, getActionsService.GetAllStoredProceduresNamesAsync());
        });

        hubConnection.On("GetStoredProcedureDefinitionAsync", (string clientId, string storedProcedureName) =>
        {
            if (!CurrentClientIsTarget(currentClientId, clientId))
            {
                return;
            }

            hubConnection.InvokeAsync("ReceiveStoredProcedureDefinitionAsync", clientId, getActionsService.GetStoredProcedureDefinitionAsync(storedProcedureName));
        });

        hubConnection.On("GetAllFunctionsNamesAsync", (string clientId) =>
        {
            if (!CurrentClientIsTarget(currentClientId, clientId))
            {
                return;
            }

            hubConnection.InvokeAsync("ReceiveAllFunctionsNamesAsync", clientId, getActionsService.GetAllFunctionsNamesAsync());
        });

        hubConnection.On("GetFunctionDefinitionAsync", (string clientId, string functionName) =>
        {
            if (!CurrentClientIsTarget(currentClientId, clientId))
            {
                return;
            }

            hubConnection.InvokeAsync("ReceiveFunctionDefinitionAsync", clientId, getActionsService.GetFunctionDefinitionAsync(functionName));
        });

        hubConnection.On("GetAllViewsNamesAsync", (string clientId) =>
        {
            if (!CurrentClientIsTarget(currentClientId, clientId))
            {
                return;
            }

            hubConnection.InvokeAsync("ReceiveAllViewsNamesAsync", clientId, getActionsService.GetAllViewsNamesAsync());
        });

        hubConnection.On("GetViewDefinitionAsync", (string clientId, string viewName) =>
        {
            if (!CurrentClientIsTarget(currentClientId, clientId))
            {
                return;
            }

            hubConnection.InvokeAsync("ReceiveViewDefinitionAsync", clientId, getActionsService.GetViewDefinitionAsync(viewName));
        });

        hubConnection.On("GetDbTablesStructureAsync", (string clientId) =>
        {
            if (!CurrentClientIsTarget(currentClientId, clientId))
            {
                return;
            }

            hubConnection.InvokeAsync("ReceiveDbTablesStructureAsync", clientId, getActionsService.GetDbTablesStructureAsync());
        });

        hubConnection.On("GetDbTablesCheckAndUniqueConstraintsAsync", (string clientId) =>
        {
            if (!CurrentClientIsTarget(currentClientId, clientId))
            {
                return;
            }

            hubConnection.InvokeAsync("ReceiveDbTablesCheckAndUniqueConstraintsAsync", clientId, getActionsService.GetDbTablesCheckAndUniqueConstraintsAsync());
        });

        hubConnection.On("GetStoredProceduresWithDetailAsync", (string clientId) =>
        {
            if (!CurrentClientIsTarget(currentClientId, clientId))
            {
                return;
            }

            hubConnection.InvokeAsync("ReceiveStoredProceduresWithDetailAsync", clientId, getActionsService.GetStoredProceduresWithDetailAsync());
        });

        hubConnection.On("GetFunctionsWithDetailAsync", (string clientId) =>
        {
            if (!CurrentClientIsTarget(currentClientId, clientId))
            {
                return;
            }

            hubConnection.InvokeAsync("ReceiveFunctionsWithDetailAsync", clientId, getActionsService.GetFunctionsWithDetailAsync());
        });

        hubConnection.On("GetViewsWithDetailAsync", (string clientId) =>
         {
             if (!CurrentClientIsTarget(currentClientId, clientId))
             {
                 return;
             }

             hubConnection.InvokeAsync("ReceiveViewsWithDetailAsync", clientId, getActionsService.GetViewsWithDetailAsync());
        });

        hubConnection.On("GetUserDefinedTypesWithDefaultsAndRulesAndDefinitionAsync", (string clientId) =>
        {
            if (!CurrentClientIsTarget(currentClientId, clientId))
            {
                return;
            }

            hubConnection.InvokeAsync("ReceiveUserDefinedTypesWithDefaultsAndRulesAndDefinitionAsync", clientId, getActionsService.GetUserDefinedTypesWithDefaultsAndRulesAndDefinitionAsync());
        });

        hubConnection.On("GetUserDefinedTableTypesAsync", (string clientId) =>
        {
            if (!CurrentClientIsTarget(currentClientId, clientId))
            {
                return;
            }

            hubConnection.InvokeAsync("ReceiveUserDefinedTableTypesAsync", clientId, getActionsService.GetUserDefinedTableTypesAsync());
        });
    }
}