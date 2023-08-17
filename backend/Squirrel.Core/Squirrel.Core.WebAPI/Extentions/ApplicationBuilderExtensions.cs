using Squirrel.Core.DAL.Context;
using Microsoft.EntityFrameworkCore;

namespace Squirrel.Core.WebAPI.Extentions
{
    public static class ApplicationBuilderExtensions
    {
        public static void UseCodiCoreContext(this IApplicationBuilder app)
        {
            using var scope = app.ApplicationServices.GetService<IServiceScopeFactory>()?.CreateScope();
            using var context = scope?.ServiceProvider.GetRequiredService<SquirrelCoreContext>();
            context?.Database.Migrate();
        }
    }
}
