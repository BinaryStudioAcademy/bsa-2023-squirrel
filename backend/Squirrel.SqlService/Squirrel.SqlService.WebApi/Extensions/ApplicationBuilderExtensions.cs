using Squirrel.SqlService.BLL.Hubs;

namespace Squirrel.SqlService.WebApi.Extensions;

public static class ApplicationBuilderExtensions
{
    public static void UseConsoleAppHub(this WebApplication app)
    {
        var consoleAppHubName = "ConsoleAppHub";
        app.MapHub<ConsoleAppHub>(consoleAppHubName);
    }
}