using Microsoft.AspNetCore.Builder;
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
            hubConnection.InvokeAsync("ReceiveAllTablesNamesAsync", clientId, getActionsService.GetAllTablesNamesAsync().Result);
        });

        hubConnection.On("GetTableDataAsync", (string clientId, string tableName, int rowsCount) =>
        {
            var getActionsService = app.ApplicationServices.GetRequiredService<IGetActionsService>();
            hubConnection.InvokeAsync("ReceiveTableDataAsync", clientId, getActionsService.GetTableDataAsync(tableName, rowsCount).Result);
        });

        hubConnection.On("GetAllStoredProceduresNamesAsync", (string clientId) =>
        {
            var getActionsService = app.ApplicationServices.GetRequiredService<IGetActionsService>();
            hubConnection.InvokeAsync("ReceiveAllStoredProceduresNamesAsync", clientId, getActionsService.GetAllStoredProceduresNamesAsync().Result);
        });

        hubConnection.On("GetStoredProcedureDefinitionAsync", (string clientId, string storedProcedureName) =>
        {
            var getActionsService = app.ApplicationServices.GetRequiredService<IGetActionsService>();
            hubConnection.InvokeAsync("ReceiveStoredProcedureDefinitionAsync", clientId, getActionsService.GetStoredProcedureDefinitionAsync(storedProcedureName).Result);
        });

        hubConnection.On("GetAllFunctionsNamesAsync", (string clientId) =>
        {
            var getActionsService = app.ApplicationServices.GetRequiredService<IGetActionsService>();
            hubConnection.InvokeAsync("ReceiveAllFunctionsNamesAsync", clientId, getActionsService.GetAllFunctionsNamesAsync().Result);
        });

        hubConnection.On("GetFunctionDefinitionAsync", (string clientId, string functionName) =>
        {
            var getActionsService = app.ApplicationServices.GetRequiredService<IGetActionsService>();
            hubConnection.InvokeAsync("ReceiveFunctionDefinitionAsync", clientId, getActionsService.GetFunctionDefinitionAsync(functionName).Result);
        });

        hubConnection.On("GetAllViewsNamesAsync", (string clientId) =>
        {
            var getActionsService = app.ApplicationServices.GetRequiredService<IGetActionsService>();
            hubConnection.InvokeAsync("ReceiveAllViewsNamesAsync", clientId, getActionsService.GetAllViewsNamesAsync().Result);
        });

        hubConnection.On("GetViewDefinitionAsync", (string clientId, string viewName) =>
        {
            var getActionsService = app.ApplicationServices.GetRequiredService<IGetActionsService>();
            hubConnection.InvokeAsync("ReceiveViewDefinitionAsync", clientId, getActionsService.GetViewDefinitionAsync(viewName).Result);
        });

        hubConnection.On("GetDbTablesStructureAsync", (string clientId) =>
        {
            var getActionsService = app.ApplicationServices.GetRequiredService<IGetActionsService>();
            hubConnection.InvokeAsync("ReceiveDbTablesStructureAsync", clientId, getActionsService.GetDbTablesStructureAsync().Result);
        });

        hubConnection.On("GetDbTablesCheckAndUniqueConstraintsAsync", (string clientId) =>
        {
            var getActionsService = app.ApplicationServices.GetRequiredService<IGetActionsService>();
            hubConnection.InvokeAsync("ReceiveDbTablesCheckAndUniqueConstraintsAsync", clientId, getActionsService.GetDbTablesCheckAndUniqueConstraintsAsync().Result);
        });

        hubConnection.On("GetStoredProceduresWithDetailAsync", (string clientId) =>
        {
            var getActionsService = app.ApplicationServices.GetRequiredService<IGetActionsService>();
            hubConnection.InvokeAsync("ReceiveStoredProceduresWithDetailAsync", clientId, getActionsService.GetStoredProceduresWithDetailAsync().Result);
        });

        hubConnection.On("GetFunctionsWithDetailAsync", (string clientId) =>
        {
            var getActionsService = app.ApplicationServices.GetRequiredService<IGetActionsService>();
            hubConnection.InvokeAsync("ReceiveFunctionsWithDetailAsync", clientId, getActionsService.GetFunctionsWithDetailAsync().Result);
        });

        hubConnection.On("GetViewsWithDetailAsync", (string clientId) =>
         {
             var getActionsService = app.ApplicationServices.GetRequiredService<IGetActionsService>();
             hubConnection.InvokeAsync("ReceiveViewsWithDetailAsync", clientId, getActionsService.GetViewsWithDetailAsync().Result);
        });

        hubConnection.On("GetUserDefinedTypesWithDefaultsAndRulesAndDefinitionAsync", (string clientId) =>
        {
            var getActionsService = app.ApplicationServices.GetRequiredService<IGetActionsService>();
            hubConnection.InvokeAsync("ReceiveUserDefinedTypesWithDefaultsAndRulesAndDefinitionAsync", clientId, getActionsService.GetUserDefinedTypesWithDefaultsAndRulesAndDefinitionAsync().Result);
        });

        hubConnection.On("GetUserDefinedTableTypesAsync", (string clientId) =>
        {
            var getActionsService = app.ApplicationServices.GetRequiredService<IGetActionsService>();
            hubConnection.InvokeAsync("ReceiveUserDefinedTableTypesAsync", clientId, getActionsService.GetUserDefinedTableTypesAsync().Result);
        });
    }
}