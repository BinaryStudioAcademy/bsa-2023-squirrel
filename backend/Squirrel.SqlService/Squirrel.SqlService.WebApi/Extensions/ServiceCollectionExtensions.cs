using Microsoft.Extensions.Options;
using Squirrel.Core.DAL.Entities;
using Squirrel.SqlService.WebApi.Interfaces;
using Squirrel.SqlService.WebApi.Options;
using Squirrel.SqlService.WebApi.Services;

namespace Squirrel.SqlService.WebApi.Extensions;

public static class ServiceCollectionExtensions
{
    public static void RegisterCustomServices(this IServiceCollection services)
    {
        services.AddScoped<ITextService, TextService>();
    }

    public static void ConfigureCors(this IServiceCollection services, IConfiguration configuration)
    {
        var allowedOrigin = configuration.GetRequiredSection("CoreWebAPIDomain").Value;
        services.AddCors(options => 
            options.AddDefaultPolicy(policy => 
                policy
                    .AllowAnyHeader()
                    .AllowAnyMethod()
                    .WithOrigins(allowedOrigin)));
    }

    public static void AddMongoDbService(this IServiceCollection services, IConfiguration configuration)
    {
        services.Configure<MongoDatabaseConnectionSettings>(configuration.GetSection("MongoDatabase"));

        services.AddTransient<IMongoService<Sample>>(s =>
            new MongoService<Sample>(s.GetRequiredService<IOptions<MongoDatabaseConnectionSettings>>(), "SampleCollection"));
    }
}
