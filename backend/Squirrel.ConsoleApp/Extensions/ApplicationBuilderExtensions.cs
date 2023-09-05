using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.SignalR.Client;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Squirrel.ConsoleApp.BL.Interfaces;
using Squirrel.ConsoleApp.Models;

namespace Squirrel.Core.WebAPI.Extensions;

public static class ApplicationBuilderExtensions
{
    private static HubConnection? _hubConnection;
    private static string? _clientId;

    public static void InitializeFileSettings(this IApplicationBuilder app)
    {
        var fileService = app.ApplicationServices.GetRequiredService<IConnectionFileService>();
        fileService?.CreateEmptyFile();
    }

    public static void RegisterHubs(this IApplicationBuilder app, IConfiguration config)
    {
        var fileService = app.ApplicationServices.GetRequiredService<IClientIdFileService>();
        var getActionsService = app.ApplicationServices.GetRequiredService<IGetActionsService>();

        _clientId = fileService?.GetClientId();

        _hubConnection = new HubConnectionBuilder()
            .WithUrl(config.GetSection("SignalRSettings")["HubConnectionUrl"])
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

        // DELETE: Just for testing
        _hubConnection.On("ExecuteQuery", (string clientId) =>
        {
            if (string.IsNullOrEmpty(_clientId))
            {
                return;
            }

            if (!clientId.Equals(_clientId))
            {
                return;
            }

            _hubConnection.InvokeAsync("ReceiveData", clientId, ExecuteQuery());


            _hubConnection.RegisterActions(_clientId, getActionsService);

            _hubConnection.StartAsync();
        });
    }

    // DELETE: Temporary seed data
    private static QueryResultTable ExecuteQuery()
    {
        QueryResultTable tmp = new("Column1", "Column2", "Column3", "Column4", "Column5");
        tmp.AddRow("val11", "val12", "val13", "val14", "val15");
        tmp.AddRow("val21", "val22", "val23", "val24", "val25");
        tmp.AddRow("val31", "val32", "val33", "val34", "val35");

        return tmp;
    }
}