using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Newtonsoft.Json.Converters;
using Squirrel.ConsoleApp.BL.Interfaces;
using Squirrel.ConsoleApp.BL.Services;
using Squirrel.ConsoleApp.Filters;
using Squirrel.ConsoleApp.Models;
using Squirrel.ConsoleApp.Services;
using Squirrel.Core.WebAPI.Extensions;
using Squirrel.Core.WebAPI.Validators.Project;

namespace Squirrel.ConsoleApp;

public class Startup
{
    public static IConfiguration Configuration { get; } = new ConfigurationBuilder()
        .SetBasePath(AppContext.BaseDirectory)
        .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
        .AddJsonFile(FilePathHelperService.GetDbSettingsFilePath(), optional: true, reloadOnChange: true)
        .Build();

    public void ConfigureServices(IServiceCollection services)
    {
        services.Configure<DbSettings>(Configuration.GetSection(nameof(DbSettings)));
        var serviceProvider = services.BuildServiceProvider();
        var dbSettings = serviceProvider.GetRequiredService<IOptionsSnapshot<DbSettings>>().Value;

        Console.WriteLine($"DB Settings:\n - DbType: {dbSettings.DbType}\n - Connection string: {dbSettings.ConnectionString}");
       
        services.AddScoped<IDbQueryProvider>(c => DatabaseFactory.CreateDbQueryProvider(dbSettings.DbType));
        services.AddScoped<IDatabaseService>(c => DatabaseFactory.CreateDatabaseService(dbSettings.DbType, dbSettings.ConnectionString));

        services.AddSingleton<IConnectionFileService, ConnectionFileService>();
        services.AddSingleton<IClientIdFileService, ClientIdFileService>();

        services.AddTransient<IGetActionsService, GetActionsService>();
        
        services.AddControllers().AddFluentValidation(fv => fv.RegisterValidatorsFromAssemblyContaining<DbSettingsValidator>());

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