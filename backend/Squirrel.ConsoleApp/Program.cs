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
        foreach (var procedure in procedures.Data)
        {
            var tableData = await service.GetStoredProcedureAsync(procedure);

            Console.WriteLine($"tableData.Name: {tableData.Name}");
            Console.WriteLine($"tableData.Type: {tableData.Type}");
            Console.WriteLine($"tableData.Data:");
            Console.WriteLine(string.Join(Environment.NewLine, tableData.Data.Select(e => string.Join(", ", e))));
            Console.WriteLine("=============================");
        }
    }

    private static async Task DisplayTablesAsync(IGetActionsService service)
    {
        var tables = await service.GetAllTablesAsync();
        foreach (var tableName in tables.Data)
        {
            var tableData = await service.GetTableDataAsync(tableName, 10);

            Console.WriteLine($"tableData.Name: {tableData.Name}");
            Console.WriteLine($"tableData.Type: {tableData.Type}");
            Console.WriteLine($"tableData.Data:");
            Console.WriteLine(string.Join(" | ", tableData.Table.ColumnNames));
            Console.WriteLine(string.Join(Environment.NewLine, tableData.Table.Rows.Select(e => string.Join(", ", e))));
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

    public static DatabaseType ParseDatabaseType(string value)
    {
        if (!Enum.TryParse(value, out DatabaseType dbType))
            throw new ArgumentException("Invalid DatabaseType provided.");

        return dbType;
    }

    public static ServiceProvider BuildServiceProvider(DatabaseType databaseType)
    {
        var services = new ServiceCollection();

        switch (databaseType)
        {
            case DatabaseType.SqlServer:
                services.AddSingleton<IDbQueryProvider, SqlServerQueryProvider>();
                services.AddTransient<IDatabaseService, SqlServerService>();
                break;
            case DatabaseType.PostgreSQL:
                services.AddSingleton<IDbQueryProvider, PostgreSqlQueryProvider>();
                services.AddTransient<IDatabaseService, PostgreSqlService>();
                break;
            default:
                throw new NotImplementedException($"Database type {databaseType} is not supported.");
        }

        return services.BuildServiceProvider();
    }
}
