using Serilog;

namespace Squirrel.Core.WebAPI.Extensions;

public static class WebApplicationBuilderExtensions
{
    public static void AddSerilog(this WebApplicationBuilder builder)
    {
        var logger = new LoggerConfiguration()
                     .ReadFrom!.Configuration(builder.Configuration)!
                     .Enrich!.FromLogContext()!
                     .CreateLogger()!;
        builder.Logging.AddSerilog(logger);
    }
}