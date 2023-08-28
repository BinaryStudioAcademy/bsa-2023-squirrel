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
        // This is for testing needs
        try
        {
            var connection = Configuration.GetConnectionString("SquirrelCoreDBConnection");
            if (string.IsNullOrEmpty(connection))
            {
                Console.WriteLine("ConnectionString cannot be null or empty");
                return;
            }

            var service = new GetActionsService(DbType.SqlServer, connection);
            var procedures = await service.GetAllStoredProceduresAsync();

            if (procedures.Data == null || procedures.Data.Count == 0)
            {
                Console.WriteLine("No Tables");
                return;
            }

            foreach (var tableName in procedures.Data)
            {
                var tableData = await service.GetStoredProcedureAsync(tableName);

                Console.WriteLine($"tableData.Name: {tableData.Name}");
                Console.WriteLine($"tableData.Type: {tableData.Type}");
                Console.WriteLine($"tableData.Data:");
                Console.WriteLine(string.Join(Environment.NewLine, tableData.Data.Select(e => string.Join(", ", e))));
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
