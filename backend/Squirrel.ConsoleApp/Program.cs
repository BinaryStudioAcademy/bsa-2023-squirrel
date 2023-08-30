using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;

namespace Squirrel.ConsoleApp;

internal class Program
{
    static void Main(string[] args)
    {
        var config = new ConfigurationBuilder()
            .AddJsonFile("appsettings.json")
            .Build();

        var baseUrl = config.GetSection("WebServerSettings")["BaseUrl"];

        CreateHostBuilder(args, baseUrl).Build().Run();
    }

    public static IHostBuilder CreateHostBuilder(string[] args, string baseUrl) =>
        Host.CreateDefaultBuilder(args)
            .ConfigureWebHostDefaults(webBuilder =>
            {
                webBuilder.UseUrls(baseUrl);
                webBuilder.UseStartup<Startup>();
            });
}