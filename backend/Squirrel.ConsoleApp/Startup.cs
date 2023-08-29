using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Squirrel.ConsoleApp.BL.Interfaces;
using Squirrel.ConsoleApp.BL.Services;
using Squirrel.ConsoleApp.Filters;

namespace Squirrel.ConsoleApp;

public class Startup
{
    public void ConfigureServices(IServiceCollection services)
    {
        services.AddScoped<IConnectionFileService, ConnectionFileService>();
        
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