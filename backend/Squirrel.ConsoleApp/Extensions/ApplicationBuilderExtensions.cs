using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.SignalR.Client;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Squirrel.ConsoleApp.BL.Interfaces;
using Squirrel.ConsoleApp.Models;

namespace Squirrel.Core.WebAPI.Extensions;

public static class ApplicationBuilderExtensions
{
    public static void InitializeFileSettings(this IApplicationBuilder app)
    {
        var fileService = app.ApplicationServices.GetRequiredService<IConnectionFileService>();
        fileService?.CreateEmptyFile();
    }

    public static void RegisterHubs(this IApplicationBuilder app, IConfiguration config)
    {
        var fileService = app.ApplicationServices.GetRequiredService<IClientIdFileService>();
        var getActionsService = app.ApplicationServices.GetRequiredService<IGetActionsService>();

        var _clientId = fileService?.GetClientId() ?? string.Empty;

        var _hubConnection = new HubConnectionBuilder()
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

        _hubConnection.RegisterActions(_clientId, app);

        _hubConnection.StartAsync();
    }
}