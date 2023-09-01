using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;
using Squirrel.Core.BLL.Hubs;
using Squirrel.Core.BLL.Interfaces;
using Squirrel.Core.DAL.Context;
using Squirrel.Core.DAL.Enums;

namespace Squirrel.Core.WebAPI.Extensions;

public static class ApplicationBuilderExtensions
{
    public static void UseSquirrelCoreContext(this IApplicationBuilder app)
    {
        using var scope = app.ApplicationServices.GetService<IServiceScopeFactory>()?.CreateScope();
        using var context = scope?.ServiceProvider.GetRequiredService<SquirrelCoreContext>();
        context?.Database.Migrate();
    }

    public static void UseSquirrelHub(this WebApplication app)
    {
        app.MapPost("ExecuteQuery", async (string clientId, DbEngine dbEngine, string query, IHubContext<SquirrelHub, ISquirrelHub> context) =>
        {
            await context.Clients.All.ExecuteQuery(clientId, dbEngine, query);
            return Results.NoContent();
        });

        app.MapHub<SquirrelHub>("SquirrelHub");
    }
}