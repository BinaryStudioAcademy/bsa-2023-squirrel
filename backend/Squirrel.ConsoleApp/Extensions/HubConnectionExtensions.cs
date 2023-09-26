using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.SignalR.Client;
using Microsoft.Extensions.DependencyInjection;
using Squirrel.ConsoleApp.BL.Interfaces;
using Squirrel.ConsoleApp.Models;

namespace Squirrel.ConsoleApp.Extensions;

public static class HubConnectionExtensions
{
    public static void RegisterActions(this HubConnection hubConnection, IApplicationBuilder app)
    {
        // We get IGetActionsService in every single action because we must be sure
        // that we work with current DatabaseService and QueryProvider (from Client's settings file).
        // Every time when we get IGetActionsService, we reread settings file.
        hubConnection.On("GetAllTablesNamesAsync", async (Guid queryId) =>
        {
            var getActionsService = app.ApplicationServices.GetRequiredService<IGetActionsService>();
            await hubConnection.InvokeAsync("ProcessReceivedDataFromClientSideAsync", queryId, "GetAllTablesNamesAsync",
                await getActionsService.GetAllTablesNamesAsync());
        });

        hubConnection.On("GetTableDataAsync", async (Guid queryId, string schema, string tableName, int rowsCount) =>
        {
            var getActionsService = app.ApplicationServices.GetRequiredService<IGetActionsService>();
            await hubConnection.InvokeAsync("ProcessReceivedDataFromClientSideAsync", queryId, "GetTableDataAsync",
                await getActionsService.GetTableDataAsync(schema, tableName, rowsCount));
        });

        hubConnection.On("GetAllStoredProceduresNamesAsync", async (Guid queryId) =>
        {
            var getActionsService = app.ApplicationServices.GetRequiredService<IGetActionsService>();
            await hubConnection.InvokeAsync("ProcessReceivedDataFromClientSideAsync", queryId, "GetAllStoredProceduresNamesAsync",
                await getActionsService.GetAllStoredProceduresNamesAsync());
        });
        
        hubConnection.On("GetStoredProcedureDefinitionAsync", async (Guid queryId, string storedProcedureSchema, string storedProcedureName) =>
        {
            var getActionsService = app.ApplicationServices.GetRequiredService<IGetActionsService>();
            await hubConnection.InvokeAsync("ProcessReceivedDataFromClientSideAsync", queryId, "GetStoredProcedureDefinitionAsync",
                await getActionsService.GetStoredProcedureDefinitionAsync(storedProcedureSchema, storedProcedureName));
        });

        hubConnection.On("GetAllFunctionsNamesAsync", async (Guid queryId) =>
        {
            var getActionsService = app.ApplicationServices.GetRequiredService<IGetActionsService>();
            await hubConnection.InvokeAsync("ProcessReceivedDataFromClientSideAsync", queryId, "GetAllFunctionsNamesAsync",
                await getActionsService.GetAllFunctionsNamesAsync());
        });
        
        hubConnection.On("GetFunctionDefinitionAsync", async (Guid queryId, string functionSchema, string functionName) =>
        {
            var getActionsService = app.ApplicationServices.GetRequiredService<IGetActionsService>();
            await hubConnection.InvokeAsync("ProcessReceivedDataFromClientSideAsync", queryId, "GetFunctionDefinitionAsync",
                await getActionsService.GetFunctionDefinitionAsync(functionSchema, functionName));
        });

        hubConnection.On("GetAllViewsNamesAsync", async (Guid queryId) =>
        {
            var getActionsService = app.ApplicationServices.GetRequiredService<IGetActionsService>();
            await hubConnection.InvokeAsync("ProcessReceivedDataFromClientSideAsync", queryId, "GetAllViewsNamesAsync", 
                await getActionsService.GetAllViewsNamesAsync());
        });
        
        hubConnection.On("GetViewDefinitionAsync", async (Guid queryId, string viewSchema, string viewName) =>
        {
            var getActionsService = app.ApplicationServices.GetRequiredService<IGetActionsService>();
            await hubConnection.InvokeAsync("ProcessReceivedDataFromClientSideAsync", queryId, "GetViewDefinitionAsync",
                await getActionsService.GetViewDefinitionAsync(viewSchema, viewName));
        });

        hubConnection.On("GetTableStructureAsync", async (Guid queryId, string schema, string tableName) =>
        {
            var getActionsService = app.ApplicationServices.GetRequiredService<IGetActionsService>();
            await hubConnection.InvokeAsync("ProcessReceivedDataFromClientSideAsync", queryId, "GetTableStructureAsync",
                await getActionsService.GetTableStructureAsync(schema, tableName));
        });

        hubConnection.On("GetTableChecksAndUniqueConstraintsAsync", async (Guid queryId, string schema, string tableName) =>
        {
            var getActionsService = app.ApplicationServices.GetRequiredService<IGetActionsService>();
            await hubConnection.InvokeAsync("ProcessReceivedDataFromClientSideAsync", queryId, "GetTableChecksAndUniqueConstraintsAsync",
                await getActionsService.GetTableChecksAndUniqueConstraintsAsync(schema, tableName));
        });

        hubConnection.On("GetStoredProceduresWithDetailAsync", async (Guid queryId) =>
        {
            var getActionsService = app.ApplicationServices.GetRequiredService<IGetActionsService>();
            await hubConnection.InvokeAsync("ProcessReceivedDataFromClientSideAsync", queryId, "GetStoredProceduresWithDetailAsync",
                await getActionsService.GetStoredProceduresWithDetailAsync());
        });

        hubConnection.On("GetFunctionsWithDetailAsync", async (Guid queryId) =>
        {
            var getActionsService = app.ApplicationServices.GetRequiredService<IGetActionsService>();
            await hubConnection.InvokeAsync("ProcessReceivedDataFromClientSideAsync", queryId, "GetFunctionsWithDetailAsync",
                await getActionsService.GetFunctionsWithDetailAsync());
        });

        hubConnection.On("GetViewsWithDetailAsync", async (Guid queryId) =>
         {
             var getActionsService = app.ApplicationServices.GetRequiredService<IGetActionsService>();
             await hubConnection.InvokeAsync("ProcessReceivedDataFromClientSideAsync", queryId, "GetViewsWithDetailAsync",
                 await getActionsService.GetViewsWithDetailAsync());
        });

        hubConnection.On("GetUserDefinedTypesWithDefaultsAndRulesAndDefinitionAsync", async (Guid queryId) =>
        {
            var getActionsService = app.ApplicationServices.GetRequiredService<IGetActionsService>();
            await hubConnection.InvokeAsync("ProcessReceivedDataFromClientSide", queryId,
                "GetUserDefinedTypesWithDefaultsAndRulesAndDefinitionAsync",
                await getActionsService.GetUserDefinedTypesWithDefaultsAndRulesAndDefinitionAsync());
        });

        hubConnection.On("GetUserDefinedTableTypesAsync", async (Guid queryId) =>
        {
            var getActionsService = app.ApplicationServices.GetRequiredService<IGetActionsService>();
            await hubConnection.InvokeAsync("ProcessReceivedDataFromClientSideAsync", queryId, "GetUserDefinedTableTypesAsync",
                await getActionsService.GetUserDefinedTableTypesAsync());
        });
        
        hubConnection.On("RemoteConnectAsync", async (Guid queryId, ConnectionStringDto connectionStringDto) =>
        {
            app.ApplicationServices.GetRequiredService<IConnectionService>().TryConnect(connectionStringDto);
            await hubConnection.InvokeAsync("ProcessReceivedDataFromClientSideAsync", queryId, "RemoteConnectAsync", new QueryResultTable());
        });

        hubConnection.On("ExecuteScriptAsync", async (Guid queryId, string scriptContent) =>
        {
            var getActionsService = app.ApplicationServices.GetRequiredService<IGetActionsService>();
            await hubConnection.InvokeAsync("ProcessReceivedDataFromClientSideAsync", queryId, "ExecuteScriptAsync",
                await getActionsService.ExecuteScriptAsync(scriptContent));
        });
    }
}