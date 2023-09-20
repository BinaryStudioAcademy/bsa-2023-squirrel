using Squirrel.ConsoleApp.BL.Exceptions;
using Squirrel.ConsoleApp.BL.Interfaces;
using Squirrel.ConsoleApp.Models;

namespace Squirrel.ConsoleApp.BL.Services;

public class ConnectionService: IConnectionService
{
    private readonly IConnectionStringService _connectionStringService;
    private readonly IConnectionFileService _connectionFileService;
    private readonly IClientIdFileService _clientIdFileService;

    public ConnectionService(IConnectionStringService connectionStringService,
        IConnectionFileService connectionFileService, IClientIdFileService clientIdFileService)
    {
        _connectionStringService = connectionStringService;
        _connectionFileService = connectionFileService;
        _clientIdFileService = clientIdFileService;
    }

    public string TryConnect(ConnectionStringDto connectionStringDto)
    {
        // we need IntegratedSecurity = true to connect
        // to MSSQL local DB (it will be changed as SettingController updates)
        connectionStringDto.IntegratedSecurity = true;

        var connectionString = _connectionStringService.BuildConnectionString(connectionStringDto);
        var databaseService =
            DatabaseServiceFactory.CreateDatabaseService(connectionStringDto.DbEngine, connectionString);
        var databaseProvider = DatabaseServiceFactory.CreateDbQueryProvider(connectionStringDto.DbEngine);

        // Test connection;
        try
        {
            var testQueryResult = databaseService.ExecuteQuery(databaseProvider.GetTablesNamesQuery());
            Console.WriteLine(testQueryResult);
        }
        catch (Exception ex)
        {
            throw new DbConnectionFailed(connectionString, ex.Message);
        }

        _connectionFileService.SaveToFile(connectionStringDto);

        return _clientIdFileService.GetClientId();
    }
}