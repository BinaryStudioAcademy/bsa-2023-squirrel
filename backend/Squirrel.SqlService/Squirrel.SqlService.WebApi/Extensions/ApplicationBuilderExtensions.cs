using Squirrel.SqlService.BLL.Hubs;

namespace Squirrel.Core.WebAPI.Extensions;

public static class ApplicationBuilderExtensions
{
    public static void UseConsoleAppHub(this WebApplication app)
    {
        app.MapHub<ConsoleAppHub>("ConsoleAppHub");
    }
}