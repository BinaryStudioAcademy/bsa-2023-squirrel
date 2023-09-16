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
            hubConnection.InvokeAsync("ProcessReceivedDataFromClientSide", clientId, "GetAllTablesNamesAsync", getActionsService.GetAllTablesNamesAsync().Result);
        });

        hubConnection.On("GetTableDataAsync", (string clientId, string schema, string tableName, int rowsCount) =>
        {
            var getActionsService = app.ApplicationServices.GetRequiredService<IGetActionsService>();
            hubConnection.InvokeAsync("ProcessReceivedDataFromClientSide", clientId, "GetTableDataAsync", getActionsService.GetTableDataAsync(schema, tableName, rowsCount).Result);
        });

        hubConnection.On("GetAllStoredProceduresNamesAsync", (string clientId) =>
        {
            var getActionsService = app.ApplicationServices.GetRequiredService<IGetActionsService>();
            hubConnection.InvokeAsync("ProcessReceivedDataFromClientSide", clientId, "GetAllStoredProceduresNamesAsync", getActionsService.GetAllStoredProceduresNamesAsync().Result);
        });

        hubConnection.On("GetStoredProcedureDefinitionAsync", (string clientId, string storedProcedureSchema, string storedProcedureName) =>
        {
            var getActionsService = app.ApplicationServices.GetRequiredService<IGetActionsService>();
            hubConnection.InvokeAsync("ProcessReceivedDataFromClientSide", clientId, "GetStoredProcedureDefinitionAsync", getActionsService.GetStoredProcedureDefinitionAsync(storedProcedureSchema, storedProcedureName).Result);
        });

        hubConnection.On("GetAllFunctionsNamesAsync", (string clientId) =>
        {
            var getActionsService = app.ApplicationServices.GetRequiredService<IGetActionsService>();
            hubConnection.InvokeAsync("ProcessReceivedDataFromClientSide", clientId, "GetAllFunctionsNamesAsync", getActionsService.GetAllFunctionsNamesAsync().Result);
        });

        hubConnection.On("GetFunctionDefinitionAsync", (string clientId, string functionSchema, string functionName) =>
        {
            var getActionsService = app.ApplicationServices.GetRequiredService<IGetActionsService>();
            hubConnection.InvokeAsync("ProcessReceivedDataFromClientSide", clientId, "GetFunctionDefinitionAsync", getActionsService.GetFunctionDefinitionAsync(functionSchema, functionName).Result);
        });

        hubConnection.On("GetAllViewsNamesAsync", (string clientId) =>
        {
            var getActionsService = app.ApplicationServices.GetRequiredService<IGetActionsService>();
            hubConnection.InvokeAsync("ProcessReceivedDataFromClientSide", clientId, "GetAllViewsNamesAsync", getActionsService.GetAllViewsNamesAsync().Result);
        });

        hubConnection.On("GetViewDefinitionAsync", (string clientId, string viewSchema, string viewName) =>
        {
            var getActionsService = app.ApplicationServices.GetRequiredService<IGetActionsService>();
            hubConnection.InvokeAsync("ProcessReceivedDataFromClientSide", clientId, "GetViewDefinitionAsync", getActionsService.GetViewDefinitionAsync(viewSchema, viewName).Result);
        });

        hubConnection.On("GetTableStructureAsync", (string clientId, string schema, string tableName) =>
        {
            var getActionsService = app.ApplicationServices.GetRequiredService<IGetActionsService>();
            hubConnection.InvokeAsync("ProcessReceivedDataFromClientSide", clientId, "GetTableStructureAsync", getActionsService.GetTableStructureAsync(schema, tableName).Result);
        });

        hubConnection.On("GetTableChecksAndUniqueConstraintsAsync", (string clientId, string schema, string tableName) =>
        {
            var getActionsService = app.ApplicationServices.GetRequiredService<IGetActionsService>();
            hubConnection.InvokeAsync("ProcessReceivedDataFromClientSide", clientId, "GetTableChecksAndUniqueConstraintsAsync", getActionsService.GetTableChecksAndUniqueConstraintsAsync(schema, tableName).Result);
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