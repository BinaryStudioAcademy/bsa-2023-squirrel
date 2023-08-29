using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Squirrel.ConsoleApp.Interfaces;
using Squirrel.ConsoleApp.Models;
using Squirrel.ConsoleApp.Providers;
using Squirrel.ConsoleApp.Services;

namespace Squirrel.ConsoleApp;

internal class Program
{
    public static IConfiguration Configuration { get; } = new ConfigurationBuilder()
        .SetBasePath(Directory.GetCurrentDirectory())
        .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
        .Build();

    // ALL IN Program is for TESTING NEEDS 

    async static Task Main(string[] args)
    {
        try
        {
            var serviceProvider = BuildServiceProvider();
            var service = serviceProvider.GetRequiredService<IGetActionsService>();

            await DisplayStoredProceduresAsync(service);
            await DisplayTablesAsync(service);
        }
        catch (Exception ex)
        {
            DisplayExceptionDetails(ex);
        }
    }

    private static async Task DisplayStoredProceduresAsync(IGetActionsService service)
    {
        var procedures = await service.GetAllStoredProceduresNamesAsync();
        var names = procedures.Rows.SelectMany(e => e).ToList();
        foreach (var procedure in names)
        {
            var result = await service.GetStoredProcedureDefinitionAsync(procedure);

            Console.WriteLine($"result.Data: {string.Join(" | ", result.ColumnNames)}");
            Console.WriteLine(string.Join(Environment.NewLine, result.Rows.Select(e => string.Join(", ", e))));
            Console.WriteLine("=============================");
        }
    }

    private static async Task DisplayTablesAsync(IGetActionsService service)
    {
        var tables = await service.GetAllTablesNamesAsync();
        var names = tables.Rows.SelectMany(e => e).ToList();
        foreach (var tableName in names)
        {
            var tableData = await service.GetTableDataAsync(tableName, 10);

            Console.WriteLine($"tableData.Data for '{tableName}'");
            Console.WriteLine(string.Join(" | ", tableData.ColumnNames));
            Console.WriteLine(string.Join(Environment.NewLine, tableData.Rows.Select(e => string.Join(", ", e))));
            Console.WriteLine("=============================");
        }
    }

    private static void DisplayExceptionDetails(Exception ex)
    {
        Console.WriteLine($"{ex.Message}");
        Console.WriteLine("-----------------------------");
        Console.WriteLine($"{ex.InnerException?.Message}");
        Console.WriteLine("-----------------------------");
    }

    public static DbEngine ParseDatabaseType(string value)
    {
        if (!Enum.TryParse(value, out DbEngine dbType))
            throw new ArgumentException("Invalid DatabaseType provided.");

        return dbType;
    }

    public static ServiceProvider BuildServiceProvider()
    {
        var services = new ServiceCollection();
        services.Configure<DbSettings>(Configuration.GetSection(nameof(DbSettings)));
        var serviceProvider = services.BuildServiceProvider();
        var databaseType = serviceProvider.GetRequiredService<IOptions<DbSettings>>().Value.DbType;

        switch (databaseType)
        {
            case DbEngine.SqlServer:
                services.AddSingleton<IDbQueryProvider, SqlServerQueryProvider>();
                services.AddSingleton<IDatabaseService, SqlServerService>();
                break;
            case DbEngine.PostgreSQL:
                services.AddSingleton<IDbQueryProvider, PostgreSqlQueryProvider>();
                services.AddSingleton<IDatabaseService, PostgreSqlService>();
                break;
            default:
                throw new NotImplementedException($"Database type {databaseType} is not supported.");
        }

        services.AddScoped<IGetActionsService, GetActionsService>();

        return services.BuildServiceProvider();
    }


}
