using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Squirrel.ConsoleApp.BL.Extensions;
using Squirrel.ConsoleApp.BL.Interfaces;
using Squirrel.ConsoleApp.BL.Services;
using Squirrel.ConsoleApp.Filters;
using Squirrel.ConsoleApp.Models;
using Squirrel.ConsoleApp.Services;

namespace Squirrel.ConsoleApp;

public class Startup
{
    public static IConfiguration Configuration { get; } = new ConfigurationBuilder()
        .SetBasePath(AppContext.BaseDirectory)
        .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
        .Build();

    public void ConfigureServices(IServiceCollection services)
    {
        services.Configure<DbSettings>(Configuration.GetSection(nameof(DbSettings)));
        var serviceProvider = services.BuildServiceProvider();
        var dbSettings = serviceProvider.GetRequiredService<IOptionsSnapshot<DbSettings>>().Value;

        services.AddScoped<IDbQueryProvider>(c => DatabaseFactory.CreateDbQueryProvider(dbSettings.DbType));
        services.AddScoped<IDatabaseService>(c => DatabaseFactory.CreateDatabaseService(dbSettings.DbType, dbSettings.ConnectionString));

        services.AddScoped<IConnectionFileService, ConnectionFileService>();

        services.AddScoped<IGetActionsService, GetActionsService>();

        services.AddControllers(options =>
        {
            options.Filters.Add(typeof(CustomExceptionFilter));
        });

        services.AddAutoMapper();
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