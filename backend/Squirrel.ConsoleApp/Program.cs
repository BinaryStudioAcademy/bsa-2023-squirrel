using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;

namespace Squirrel.ConsoleApp;

internal class Program
{
    static void Main(string[] args)
    {
        CreateHostBuilder(args).Build().Run();
    }

    public static IHostBuilder CreateHostBuilder(string[] args) =>
        Host.CreateDefaultBuilder(args)
            .ConfigureWebHostDefaults(webBuilder =>
            {
                webBuilder.UseUrls("http://localhost:44567");
                webBuilder.UseStartup<Startup>();
            });
}