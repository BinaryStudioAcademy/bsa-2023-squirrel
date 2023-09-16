using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.SignalR.Client;
using Microsoft.Extensions.DependencyInjection;
using Squirrel.ConsoleApp.BL.Interfaces;

namespace Squirrel.ConsoleApp.Extensions;

public static class HubConnectionExtensions
{
    public static void RegisterActions(this HubConnection hubConnection, IApplicationBuilder app)
    {
        // We get IGetActionsService in every single action because we must be sure
        // that we work with current DatabaseService and QueryProvider (from Client's settings file).
        // Every time when we get IGetActionsService, we reread settings file.

        hubConnection.On("GetAllTablesNamesAsync", (Guid queryId) =>
        {
            var getActionsService = app.ApplicationServices.GetRequiredService<IGetActionsService>();
            hubConnection.InvokeAsync("ProcessReceivedDataFromClientSide", queryId, "GetAllTablesNamesAsync", getActionsService.GetAllTablesNamesAsync().Result);
        });

        hubConnection.On("GetTableDataAsync", (Guid queryId, string schema, string tableName, int rowsCount) =>
        {
            var getActionsService = app.ApplicationServices.GetRequiredService<IGetActionsService>();
            hubConnection.InvokeAsync("ProcessReceivedDataFromClientSide", queryId, "GetTableDataAsync", getActionsService.GetTableDataAsync(schema, tableName, rowsCount).Result);
        });

        hubConnection.On("GetAllStoredProceduresNamesAsync", (Guid queryId) =>
        {
            var getActionsService = app.ApplicationServices.GetRequiredService<IGetActionsService>();
            hubConnection.InvokeAsync("ProcessReceivedDataFromClientSide", queryId, "GetAllStoredProceduresNamesAsync", getActionsService.GetAllStoredProceduresNamesAsync().Result);
        });

        hubConnection.On("GetStoredProcedureDefinitionAsync", (Guid queryId, string storedProcedureName) =>
        {
            var getActionsService = app.ApplicationServices.GetRequiredService<IGetActionsService>();
            hubConnection.InvokeAsync("ProcessReceivedDataFromClientSide", queryId, "GetStoredProcedureDefinitionAsync", getActionsService.GetStoredProcedureDefinitionAsync(storedProcedureName).Result);
        });

        hubConnection.On("GetAllFunctionsNamesAsync", (Guid queryId) =>
        {
            var getActionsService = app.ApplicationServices.GetRequiredService<IGetActionsService>();
            hubConnection.InvokeAsync("ProcessReceivedDataFromClientSide", queryId, "GetAllFunctionsNamesAsync", getActionsService.GetAllFunctionsNamesAsync().Result);
        });

        hubConnection.On("GetFunctionDefinitionAsync", (Guid queryId, string functionName) =>
        {
            var getActionsService = app.ApplicationServices.GetRequiredService<IGetActionsService>();
            hubConnection.InvokeAsync("ProcessReceivedDataFromClientSide", queryId, "GetFunctionDefinitionAsync", getActionsService.GetFunctionDefinitionAsync(functionName).Result);
        });

        hubConnection.On("GetAllViewsNamesAsync", (Guid queryId) =>
        {
            var getActionsService = app.ApplicationServices.GetRequiredService<IGetActionsService>();
            hubConnection.InvokeAsync("ProcessReceivedDataFromClientSide", queryId, "GetAllViewsNamesAsync", getActionsService.GetAllViewsNamesAsync().Result);
        });

        hubConnection.On("GetViewDefinitionAsync", (Guid queryId, string viewName) =>
        {
            var getActionsService = app.ApplicationServices.GetRequiredService<IGetActionsService>();
            hubConnection.InvokeAsync("ProcessReceivedDataFromClientSide", queryId, "GetViewDefinitionAsync", getActionsService.GetViewDefinitionAsync(viewName).Result);
        });

        hubConnection.On("GetTableStructureAsync", (Guid queryId, string schema, string tableName) =>
        {
            var getActionsService = app.ApplicationServices.GetRequiredService<IGetActionsService>();
            hubConnection.InvokeAsync("ProcessReceivedDataFromClientSide", queryId, "GetTableStructureAsync", getActionsService.GetTableStructureAsync(schema, tableName).Result);
        });

        hubConnection.On("GetTableChecksAndUniqueConstraintsAsync", (Guid queryId, string schema, string tableName) =>
        {
            var getActionsService = app.ApplicationServices.GetRequiredService<IGetActionsService>();
            hubConnection.InvokeAsync("ProcessReceivedDataFromClientSide", queryId, "GetTableChecksAndUniqueConstraintsAsync", getActionsService.GetTableChecksAndUniqueConstraintsAsync(schema, tableName).Result);
        });

        hubConnection.On("GetStoredProceduresWithDetailAsync", (Guid queryId) =>
        {
            var getActionsService = app.ApplicationServices.GetRequiredService<IGetActionsService>();
            hubConnection.InvokeAsync("ProcessReceivedDataFromClientSide", queryId, "GetStoredProceduresWithDetailAsync", getActionsService.GetStoredProceduresWithDetailAsync().Result);
        });

        hubConnection.On("GetFunctionsWithDetailAsync", (Guid queryId) =>
        {
            var getActionsService = app.ApplicationServices.GetRequiredService<IGetActionsService>();
            hubConnection.InvokeAsync("ProcessReceivedDataFromClientSide", queryId, "GetFunctionsWithDetailAsync", getActionsService.GetFunctionsWithDetailAsync().Result);
        });

        hubConnection.On("GetViewsWithDetailAsync", (Guid queryId) =>
         {
             var getActionsService = app.ApplicationServices.GetRequiredService<IGetActionsService>();
             hubConnection.InvokeAsync("ProcessReceivedDataFromClientSide", queryId, "GetViewsWithDetailAsync", getActionsService.GetViewsWithDetailAsync().Result);
        });

        hubConnection.On("GetUserDefinedTypesWithDefaultsAndRulesAndDefinitionAsync", (Guid queryId) =>
        {
            var getActionsService = app.ApplicationServices.GetRequiredService<IGetActionsService>();
            hubConnection.InvokeAsync("ProcessReceivedDataFromClientSide", queryId, "GetUserDefinedTypesWithDefaultsAndRulesAndDefinitionAsync", getActionsService.GetUserDefinedTypesWithDefaultsAndRulesAndDefinitionAsync().Result);
        });

        hubConnection.On("GetUserDefinedTableTypesAsync", (Guid queryId) =>
        {
            var getActionsService = app.ApplicationServices.GetRequiredService<IGetActionsService>();
            hubConnection.InvokeAsync("ProcessReceivedDataFromClientSide", queryId, "GetUserDefinedTableTypesAsync", getActionsService.GetUserDefinedTableTypesAsync().Result);
        });
    }
}