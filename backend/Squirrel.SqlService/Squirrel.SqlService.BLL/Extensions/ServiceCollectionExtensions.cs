using System.Reflection;
using Microsoft.Extensions.DependencyInjection;
using Squirrel.SqlService.BLL.Extensions;

namespace Squirrel.SqlService.BLL.Extensions;

public static class ServiceCollectionExtensions
{
    public static void AddAutoMapper(this IServiceCollection services)
    {
        services.AddAutoMapper(Assembly.GetExecutingAssembly());
    }
}
