using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.SignalR.Client;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Squirrel.ConsoleApp.BL.Interfaces;
using Squirrel.ConsoleApp.BL.Services;
using Squirrel.ConsoleApp.Filters;
using Squirrel.ConsoleApp.Models.Models;
using Squirrel.Core.WebAPI.Extensions;

namespace Squirrel.ConsoleApp;

public class Startup
{
    public void ConfigureServices(IServiceCollection services)
    {
        services.AddScoped<IConnectionFileService, ConnectionFileService>();
        services.AddScoped<IClientIdFileService, ClientIdFileService>();

        services.AddControllers(options =>
        {
            options.Filters.Add(typeof(CustomExceptionFilter));
        });
    }
    
    public void Configure(IApplicationBuilder app, IConfiguration config)
    {
        app.UseRouting();
        app.UseHttpsRedirection();
        app.UseEndpoints(cfg =>
        {
            cfg.MapControllers();
        });

        app.InitializeFileSettings();
        app.RegisterHubs(config);
    }
}