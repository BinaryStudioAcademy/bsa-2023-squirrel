using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.SignalR.Client;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Squirrel.ConsoleApp.BL.Interfaces;

namespace Squirrel.ConsoleApp.Extensions;

public static class ApplicationBuilderExtensions
{
    private const string SignalRSettingsSection = "SignalRSettings";
    private const string HubConnectionUrl = "HubConnectionUrl";
    
    public static void InitializeFileSettings(this IApplicationBuilder app)
    {
        var fileService = app.ApplicationServices.GetRequiredService<IConnectionFileService>();
        fileService.CreateInitFile();
    }

    public static void RegisterHubs(this IApplicationBuilder app, IConfiguration config)
    {
        var fileService = app.ApplicationServices.GetRequiredService<IClientIdFileService>();

        var clientId = fileService?.GetClientId() ?? string.Empty;
        ClientIdOutput(clientId);
        
        var hubConnection = new HubConnectionBuilder()
            .WithUrl(Path.Combine(config.GetSection(SignalRSettingsSection)[HubConnectionUrl], $"?ClientId={clientId}"))
            .WithAutomaticReconnect()
            .Build();

        // Use once at OnConnectedAsync hub event
        hubConnection.On<string>("SetClientId", guid =>
        {
            var id = fileService?.GetClientId() ?? string.Empty;
            
            if (!string.IsNullOrEmpty(id))
            {
                return;
            }

            fileService?.SetClientId(guid);
            ClientIdOutput(guid);
        });

        hubConnection.RegisterActions(app);

        hubConnection.StartAsync();
    }

    private static void ClientIdOutput(string clientId)
    {
        if (!string.IsNullOrEmpty(clientId))
        {
            Console.WriteLine($"Unique key: {clientId}");
        }
    }
}