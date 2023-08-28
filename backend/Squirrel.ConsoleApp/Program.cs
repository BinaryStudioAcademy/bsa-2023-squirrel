using Microsoft.Extensions.Configuration;
using Squirrel.ConsoleApp.Models;
using Squirrel.ConsoleApp.Services;

namespace Squirrel.ConsoleApp;

internal class Program
{
    public static IConfiguration Configuration { get; } = new ConfigurationBuilder()
        .SetBasePath(Directory.GetCurrentDirectory())
        .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
        .Build();

    async static Task Main(string[] args)
    {
        try
        {
            var connection = Configuration.GetConnectionString("SquirrelCoreDBConnection");
            if (string.IsNullOrEmpty(connection))
            {
                Console.WriteLine("ConnectionString cannot be null or empty");
                return;
            }

            var service = new UserActionService(DbType.SqlServer, connection);
            var getTables = await service.GetAllTablesAsync();

            var tables = getTables.ToList();
            if (!tables.Any())
            {
                Console.WriteLine("No Tables");
                return;
            }

            foreach (var table in tables)
            {
                var tableData = await service.GetTableDataAsync(table.Name, 10);

                Console.WriteLine($"tableData.Name: {tableData.Name}");
                Console.WriteLine($"tableData.Type: {tableData.Type}");
                Console.WriteLine($"tableData.Data:");
                Console.WriteLine(tableData.Data);
                Console.WriteLine("=============================");
            }

        }
        catch (Exception ex)
        {
            Console.WriteLine($"{ex.Message}");
            Console.WriteLine("-----------------------------");
            Console.WriteLine($"{ex.InnerException?.Message}");
            Console.WriteLine("-----------------------------");
        }
    }
}
