using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json.Converters;
using Squirrel.ConsoleApp.BL.Interfaces;
using Squirrel.ConsoleApp.BL.Services;
using Squirrel.ConsoleApp.Extensions;
using Squirrel.ConsoleApp.Filters;
using Squirrel.Core.WebAPI.Validators.Project;

namespace Squirrel.ConsoleApp;

public class Startup
{
    public static IConfiguration Configuration { get; } = new ConfigurationBuilder()
        .SetBasePath(AppContext.BaseDirectory)
        .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
        .Build();

    public void ConfigureServices(IServiceCollection services)
    {
        services.AddScoped<IConnectionFileService, ConnectionFileService>();
        services.AddScoped<IClientIdFileService, ClientIdFileService>();
        services.AddScoped<IConnectionStringService, ConnectionStringService>();
        services.AddTransient<IGetActionsService, GetActionsService>();
        services.AddScoped<IJsonSerializerSettingsService, JsonSerializerSettingsService>();

        var serviceProvider = services.BuildServiceProvider();
        var connectionStringService = serviceProvider.GetRequiredService<IConnectionStringService>();
        var connectionFileService = serviceProvider.GetRequiredService<IConnectionFileService>();
       
        connectionFileService.CreateInitFile();
        var connectionString = connectionFileService.ReadFromFile();

        Console.WriteLine($"DB Settings:\n - DbType: {connectionString.DbEngine}\n - Connection string: {connectionStringService.BuildConnectionString(connectionString)}");
       
        services.AddScoped<IDbQueryProvider>(c => DatabaseServiceFactory.CreateDbQueryProvider(connectionString.DbEngine));
        services.AddScoped<IDatabaseService>(c => DatabaseServiceFactory.CreateDatabaseService(connectionString.DbEngine, connectionStringService.BuildConnectionString(connectionString)));

        services.AddControllers().AddFluentValidation(fv => fv.RegisterValidatorsFromAssemblyContaining<ConnectionStringDtoValidator>());
        
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
        app.RegisterHubs(Configuration);

        app.UseCors(builder => builder
            .AllowAnyMethod()
            .AllowAnyHeader()
            .AllowAnyOrigin());

        app.UseRouting();
        app.UseHttpsRedirection();
        app.UseEndpoints(cfg =>
        {
            cfg.MapControllers();
        });
    }
}