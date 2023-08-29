using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
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

    // TESTING NEEDS 

    async static Task Main(string[] args)
    {
        try
        {
            var dbType = ParseDatabaseType(Configuration.GetSection("DatabaseType").Value);
            var connection = Configuration.GetConnectionString("SquirrelCoreDBConnection");
            var serviceProvider = BuildServiceProvider(dbType);
            var provider = serviceProvider.GetRequiredService<IDbQueryProvider>();
            var service = new GetActionsService(dbType, provider, connection);

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
        var procedures = await service.GetAllStoredProceduresAsync();
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
        var tables = await service.GetAllTablesAsync();
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

    public static ServiceProvider BuildServiceProvider(DbEngine databaseType)
    {
        var services = new ServiceCollection();

        switch (databaseType)
        {
            case DbEngine.SqlServer:
                services.AddSingleton<IDbQueryProvider, SqlServerQueryProvider>();
                services.AddTransient<IDatabaseService, SqlServerService>();
                break;
            case DbEngine.PostgreSQL:
                services.AddSingleton<IDbQueryProvider, PostgreSqlQueryProvider>();
                services.AddTransient<IDatabaseService, PostgreSqlService>();
                break;
            default:
                throw new NotImplementedException($"Database type {databaseType} is not supported.");
        }

        return services.BuildServiceProvider();
    }
}
