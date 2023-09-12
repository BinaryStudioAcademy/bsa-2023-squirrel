using System.Reflection;
using Microsoft.Extensions.DependencyInjection;

namespace Squirrel.ConsoleApp.BL.Extensions;

public static class ServiceCollectionExtensions
{
    public static void AddAutoMapper(this IServiceCollection services)
    {
        services.AddAutoMapper(Assembly.GetExecutingAssembly());
    }
}
