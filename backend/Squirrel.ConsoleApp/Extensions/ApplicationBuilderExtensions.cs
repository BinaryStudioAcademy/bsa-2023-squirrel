using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.SignalR.Client;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Squirrel.ConsoleApp.BL.Interfaces;
using Squirrel.ConsoleApp.Models.Models;

namespace Squirrel.Core.WebAPI.Extensions;

public static class ApplicationBuilderExtensions
{
    private static HubConnection? _hubConnection;
    private static string? _clientId;

    public static void InitializeFileSettings(this IApplicationBuilder app)
    {
        using var scope = app.ApplicationServices.GetService<IServiceScopeFactory>()?.CreateScope();
        var fileService = scope?.ServiceProvider.GetRequiredService<IConnectionFileService>();
        fileService?.CreateEmptyFile();
    }

    public static void RegisterHubs(this IApplicationBuilder app, IConfiguration config)
    {
        using var scope = app.ApplicationServices.GetService<IServiceScopeFactory>()?.CreateScope();
        var fileService = scope?.ServiceProvider.GetRequiredService<IClientIdFileService>();

        _clientId = fileService?.GetClientId();

        _hubConnection = new HubConnectionBuilder()
            .WithUrl(config.GetSection("SignalRSettings")["ConnectionUrl"])
            .WithAutomaticReconnect()
            .Build();

        // Use once at OnConnectedAsync hub event
        _hubConnection.On<string>("SetClientId", (guid) =>
        {
            if (!string.IsNullOrEmpty(_clientId))
            {
                return;
            }
            fileService?.SetClientId(guid);
            _clientId = guid;
        });


        _hubConnection.On<string, DbEngine, string>("ExecuteQuery", (clientId, dbEngine, query) =>
        {
            if (string.IsNullOrEmpty(_clientId))
            {
                return;
            }

            if (!clientId.Equals(_clientId))
            {
                return;
            }

            // TODO: implement logic for executing query using different DB Services
            _hubConnection.InvokeAsync("ReceiveData", clientId, query, dbEngine, ExecuteQuery());

        });

        _hubConnection.StartAsync();
    }

    // Temporary seed data
    private static QueryResultTable ExecuteQuery()
    {
        QueryResultTable tmp = new("Column1", "Column2", "Column3", "Column4", "Column5");
        tmp.AddRow("val11", "val12", "val13", "val14", "val15");
        tmp.AddRow("val21", "val22", "val23", "val24", "val25");
        tmp.AddRow("val31", "val32", "val33", "val34", "val35");

        return tmp;
    }
}