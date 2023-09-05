using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Newtonsoft.Json.Converters;
using Squirrel.ConsoleApp.BL.Interfaces;
using Squirrel.ConsoleApp.BL.Services;
using Squirrel.ConsoleApp.Filters;
using Squirrel.ConsoleApp.Models;
using Squirrel.ConsoleApp.Providers;
using Squirrel.ConsoleApp.Services;
using Squirrel.Core.WebAPI.Extensions;

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
        var dbSettings = serviceProvider.GetRequiredService<IOptions<DbSettings>>().Value;

        Console.WriteLine($"DB Settings:\n - DbType: {dbSettings.DbType}\n - Connection string: {dbSettings.ConnectionString}");

        switch (dbSettings.DbType)
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
                // This type of error is handled in controller
                services.AddSingleton<IDbQueryProvider, SqlServerQueryProvider>();
                services.AddSingleton<IDatabaseService, SqlServerService>();
                break;
        }

        services.AddScoped<IConnectionFileService, ConnectionFileService>();
        services.AddScoped<IClientIdFileService, ClientIdFileService>();

        services.AddScoped<IGetActionsService, GetActionsService>();

        services.AddControllers(options =>
        {
            options.Filters.Add(typeof(CustomExceptionFilter));
        });

        services.AddControllers().AddNewtonsoftJson(jsonOptions =>
        {
            jsonOptions.SerializerSettings.Converters.Add(new StringEnumConverter());
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

        app.InitializeFileSettings();
        app.RegisterHubs(Configuration);
    }
}