using Squirrel.Core.BLL.Hubs;

namespace Squirrel.Core.WebAPI.Extensions;

public static class ApplicationBuilderExtensions
{
    public static void UseSquirrelHub(this WebApplication app)
    {
        app.MapHub<ConsoleAppHub>("SquirrelHub");
    }
}