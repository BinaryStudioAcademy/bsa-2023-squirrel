﻿using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.SignalR.Client;
using Microsoft.Extensions.DependencyInjection;
using Squirrel.ConsoleApp.BL.Interfaces;

namespace Squirrel.Core.WebAPI.Extensions;

public static class HubConnectionExtensions
{
    public static void RegisterActions(this HubConnection hubConnection, IApplicationBuilder app)
    {
        // We get IGetActionsService in every single action because we must be sure
        // that we work with current DatabaseService and QueryProvider (from Client's settings file).
        // Every time when we get IGetActionsService, we reread settings file.

        hubConnection.On("GetAllTablesNamesAsync", (string clientId) =>
        {
            var getActionsService = app.ApplicationServices.GetRequiredService<IGetActionsService>();
            hubConnection.InvokeAsync("ProcessReceivedDataFromClientSide", clientId, "GetAllTablesNamesAsync", getActionsService.GetAllTablesNamesAsync().Result);
        });

        hubConnection.On("GetTableDataAsync", (string clientId, string tableName, int rowsCount) =>
        {
            var getActionsService = app.ApplicationServices.GetRequiredService<IGetActionsService>();
            hubConnection.InvokeAsync("ProcessReceivedDataFromClientSide", clientId, "GetTableDataAsync", getActionsService.GetTableDataAsync(tableName, rowsCount).Result);
        });

        hubConnection.On("GetAllStoredProceduresNamesAsync", (string clientId) =>
        {
            var getActionsService = app.ApplicationServices.GetRequiredService<IGetActionsService>();
            hubConnection.InvokeAsync("ProcessReceivedDataFromClientSide", clientId, "GetAllStoredProceduresNamesAsync", getActionsService.GetAllStoredProceduresNamesAsync().Result);
        });

        hubConnection.On("GetStoredProcedureDefinitionAsync", (string clientId, string storedProcedureName) =>
        {
            var getActionsService = app.ApplicationServices.GetRequiredService<IGetActionsService>();
            hubConnection.InvokeAsync("ProcessReceivedDataFromClientSide", clientId, "GetStoredProcedureDefinitionAsync", getActionsService.GetStoredProcedureDefinitionAsync(storedProcedureName).Result);
        });

        hubConnection.On("GetAllFunctionsNamesAsync", (string clientId) =>
        {
            var getActionsService = app.ApplicationServices.GetRequiredService<IGetActionsService>();
            hubConnection.InvokeAsync("ProcessReceivedDataFromClientSide", clientId, "GetAllFunctionsNamesAsync", getActionsService.GetAllFunctionsNamesAsync().Result);
        });

        hubConnection.On("GetFunctionDefinitionAsync", (string clientId, string functionName) =>
        {
            var getActionsService = app.ApplicationServices.GetRequiredService<IGetActionsService>();
            hubConnection.InvokeAsync("ProcessReceivedDataFromClientSide", clientId, "GetFunctionDefinitionAsync", getActionsService.GetFunctionDefinitionAsync(functionName).Result);
        });

        hubConnection.On("GetAllViewsNamesAsync", (string clientId) =>
        {
            var getActionsService = app.ApplicationServices.GetRequiredService<IGetActionsService>();
            hubConnection.InvokeAsync("ProcessReceivedDataFromClientSide", clientId, "GetAllViewsNamesAsync", getActionsService.GetAllViewsNamesAsync().Result);
        });

        hubConnection.On("GetViewDefinitionAsync", (string clientId, string viewName) =>
        {
            var getActionsService = app.ApplicationServices.GetRequiredService<IGetActionsService>();
            hubConnection.InvokeAsync("ProcessReceivedDataFromClientSide", clientId, "GetViewDefinitionAsync", getActionsService.GetViewDefinitionAsync(viewName).Result);
        });

        hubConnection.On("GetDbTablesStructureAsync", (string clientId) =>
        {
            var getActionsService = app.ApplicationServices.GetRequiredService<IGetActionsService>();
            hubConnection.InvokeAsync("ProcessReceivedDataFromClientSide", clientId, "GetDbTablesStructureAsync", getActionsService.GetDbTablesStructureAsync().Result);
        });

        hubConnection.On("GetDbTablesCheckAndUniqueConstraintsAsync", (string clientId) =>
        {
            var getActionsService = app.ApplicationServices.GetRequiredService<IGetActionsService>();
            hubConnection.InvokeAsync("ProcessReceivedDataFromClientSide", clientId, "GetDbTablesCheckAndUniqueConstraintsAsync", getActionsService.GetDbTablesCheckAndUniqueConstraintsAsync().Result);
        });

        hubConnection.On("GetStoredProceduresWithDetailAsync", (string clientId) =>
        {
            var getActionsService = app.ApplicationServices.GetRequiredService<IGetActionsService>();
            hubConnection.InvokeAsync("ProcessReceivedDataFromClientSide", clientId, "GetStoredProceduresWithDetailAsync", getActionsService.GetStoredProceduresWithDetailAsync().Result);
        });

        hubConnection.On("GetFunctionsWithDetailAsync", (string clientId) =>
        {
            var getActionsService = app.ApplicationServices.GetRequiredService<IGetActionsService>();
            hubConnection.InvokeAsync("ProcessReceivedDataFromClientSide", clientId, "GetFunctionsWithDetailAsync", getActionsService.GetFunctionsWithDetailAsync().Result);
        });

        hubConnection.On("GetViewsWithDetailAsync", (string clientId) =>
         {
             var getActionsService = app.ApplicationServices.GetRequiredService<IGetActionsService>();
             hubConnection.InvokeAsync("ProcessReceivedDataFromClientSide", clientId, "GetViewsWithDetailAsync", getActionsService.GetViewsWithDetailAsync().Result);
        });

        hubConnection.On("GetUserDefinedTypesWithDefaultsAndRulesAndDefinitionAsync", (string clientId) =>
        {
            var getActionsService = app.ApplicationServices.GetRequiredService<IGetActionsService>();
            hubConnection.InvokeAsync("ProcessReceivedDataFromClientSide", clientId, "GetUserDefinedTypesWithDefaultsAndRulesAndDefinitionAsync", getActionsService.GetUserDefinedTypesWithDefaultsAndRulesAndDefinitionAsync().Result);
        });

        hubConnection.On("GetUserDefinedTableTypesAsync", (string clientId) =>
        {
            var getActionsService = app.ApplicationServices.GetRequiredService<IGetActionsService>();
            hubConnection.InvokeAsync("ProcessReceivedDataFromClientSide", clientId, "GetUserDefinedTableTypesAsync", getActionsService.GetUserDefinedTableTypesAsync().Result);
        });
    }
}