using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.SignalR.Client;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Squirrel.ConsoleApp.BL.Interfaces;

namespace Squirrel.ConsoleApp.Extensions;

public static class ApplicationBuilderExtensions
{
    public static void InitializeFileSettings(this IApplicationBuilder app)
    {
        var fileService = app.ApplicationServices.GetRequiredService<IConnectionFileService>();
        fileService?.CreateInitFile();
    }

    public static void RegisterHubs(this IApplicationBuilder app, IConfiguration config)
    {
        var fileService = app.ApplicationServices.GetRequiredService<IClientIdFileService>();

        var _clientId = fileService?.GetClientId() ?? string.Empty;

        var _hubConnection = new HubConnectionBuilder()
            .WithUrl(Path.Combine(config.GetSection("SignalRSettings")["HubConnectionUrl"], $"?ClientId={_clientId}"))
            .WithAutomaticReconnect()
            .Build();

        // Use once at OnConnectedAsync hub event
        _hubConnection.On<string>("SetClientId", (guid) =>
        {
            var _clientId = fileService?.GetClientId() ?? string.Empty;
            
            if (!string.IsNullOrEmpty(_clientId))
            {
                return;
            }

            fileService?.SetClientId(guid);
        });

        _hubConnection.RegisterActions(app);

        _hubConnection.StartAsync();
    }
}