using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Squirrel.Core.DAL.Context;

namespace Squirrel.Core.DAL.Extensions;

public static class ServiceCollectionExtensions
{
    public static void AddSquirrelCoreContext(this IServiceCollection services, IConfiguration configuration)
    {
        var connectionsString = configuration.GetConnectionString("SquirrelCoreDBConnection");
        services.AddDbContext<SquirrelCoreContext>(options =>
            options.UseSqlServer(
                connectionsString,
                opt => opt.MigrationsAssembly(typeof(SquirrelCoreContext).Assembly.GetName().Name)));
    }
}
