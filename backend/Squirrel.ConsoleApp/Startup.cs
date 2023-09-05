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
        var dbSettings = serviceProvider.GetRequiredService<IOptionsSnapshot<DbSettings>>().Value;

        Console.WriteLine($"DB Settings:\n - DbType: {dbSettings.DbType}\n - Connection string: {dbSettings.ConnectionString}");
       
        services.AddScoped<IDbQueryProvider>(c => DatabaseFactory.CreateDbQueryProvider(dbSettings.DbType));
        services.AddScoped<IDatabaseService>(c => DatabaseFactory.CreateDatabaseService(dbSettings.DbType, dbSettings.ConnectionString));

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