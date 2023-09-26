using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;

namespace Squirrel.ConsoleApp;

internal class Program
{
    public const string AppSettingsFileName = "appsettings.json";
    private const string WebServerSettingsSection = "WebServerSettings";
    private const string BaseUrl = "BaseUrl";
    
    public static void Main(string[] args)
    {
        var config = new ConfigurationBuilder()
            .AddJsonFile(AppSettingsFileName)
            .Build();

        var baseUrl = config.GetSection(WebServerSettingsSection)[BaseUrl];

        CreateHostBuilder(args, baseUrl).Build().Run();
    }

    public static IHostBuilder CreateHostBuilder(string[] args, string baseUrl)
    {
        return Host.CreateDefaultBuilder(args)
            .ConfigureWebHostDefaults(webBuilder =>
            {
                webBuilder.UseUrls(baseUrl);
                webBuilder.UseStartup<Startup>();
            });
    }
}