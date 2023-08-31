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

namespace Squirrel.ConsoleApp;

public class Startup
{
    public static IConfiguration Configuration { get; } = new ConfigurationBuilder()
        .SetBasePath(Directory.GetCurrentDirectory())
        .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
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
                throw new NotImplementedException($"Database type {databaseType} is not supported.");
        }

        services.AddScoped<IConnectionFileService, ConnectionFileService>();

        services.AddScoped<IGetActionsService, GetActionsService>();

        services.AddControllers(options =>
        {
            options.Filters.Add(typeof(CustomExceptionFilter));
        });
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