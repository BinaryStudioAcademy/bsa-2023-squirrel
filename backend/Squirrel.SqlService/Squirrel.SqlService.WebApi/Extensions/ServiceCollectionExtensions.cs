using Squirrel.SqlService.WebApi.Interfaces;
using Squirrel.SqlService.WebApi.Services;

namespace Squirrel.SqlService.WebApi.Extensions;

public static class ServiceCollectionExtensions
{
    public static void RegisterCustomServices(this IServiceCollection services)
    {
        services.AddScoped<ITextService, TextService>();
    }
}
