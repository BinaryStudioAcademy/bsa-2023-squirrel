using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Squirrel.ConsoleApp.BL.Interfaces;
using Squirrel.ConsoleApp.BL.Services;
using Squirrel.ConsoleApp.Filters;
using Squirrel.ConsoleApp.Models;
using Squirrel.ConsoleApp.Providers;
using Squirrel.ConsoleApp.Services;
using System.Text.Json.Serialization;

namespace Squirrel.ConsoleApp;

public class Startup
{
    public static IConfiguration Configuration { get; } = new ConfigurationBuilder()
        .SetBasePath(AppContext.BaseDirectory)
        .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
        .AddJsonFile(HelperService.GetDbSettingsFilePath(), optional: true, reloadOnChange: true)
        .Build();

    public void ConfigureServices(IServiceCollection services)
    {
        services.Configure<DbSettings>(Configuration.GetSection(nameof(DbSettings)));
        var serviceProvider = services.BuildServiceProvider();
        var databaseType = serviceProvider.GetRequiredService<IOptions<DbSettings>>().Value.DbType;

        switch (databaseType)
        {
            case DbEngine.SqlServer:
                services.AddSingleton<IDbQueryProvider, SqlServerQueryProvider>();
                services.AddSingleton<IDatabaseService, SqlServerService>();
                break;
            case DbEngine.PostgreSql:
                services.AddSingleton<IDbQueryProvider, PostgreSqlQueryProvider>();
                services.AddSingleton<IDatabaseService, PostgreSqlService>();
                break;
            default:
                // When the app is first launched, we create an empty DbSettings file
                // We don't have a DbType value, but the User can change the DbSettings file using endpoints
                // This is why we have to ensure that the application works even if we have the wrong DbType
                // This type of error is handled in provider services
                services.AddSingleton<IDbQueryProvider, SqlServerQueryProvider>();
                services.AddSingleton<IDatabaseService, SqlServerService>();
                break;
        }

        services.AddScoped<IConnectionFileService, ConnectionFileService>();

        services.AddScoped<IGetActionsService, GetActionsService>();

        services.AddControllers(options =>
        {
            options.Filters.Add(typeof(CustomExceptionFilter));
        });
        services.AddControllers().AddJsonOptions(options =>
            options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter()));
    }
    
    public void Configure(IApplicationBuilder app)
    {
        app.UseRouting();
        app.UseHttpsRedirection();
        app.UseEndpoints(cfg =>
        {
            cfg.MapControllers();
        });
        
        InitializeFileSettings(app);
    }

    private static void InitializeFileSettings(IApplicationBuilder app)
    {
        using var scope = app.ApplicationServices.GetService<IServiceScopeFactory>()?.CreateScope();
        var fileService = scope?.ServiceProvider.GetRequiredService<IConnectionFileService>();
        fileService?.CreateEmptyFile();
    }
}