using Newtonsoft.Json;
using Squirrel.ConsoleApp.BL.Exceptions;
using Squirrel.ConsoleApp.BL.Interfaces;
using Squirrel.ConsoleApp.Models;

namespace Squirrel.ConsoleApp.BL.Services;

public class ConnectionFileService : IConnectionFileService
{
    private readonly IJsonSerializerSettingsService _jsonSettingsService;

    public ConnectionFileService(IJsonSerializerSettingsService jsonSettingsService)
    {
        _jsonSettingsService = jsonSettingsService;
    }

    public void CreateInitFile()
    {
        var filePath = ConnectionFilePath;
        if (!File.Exists(filePath))
        {
            File.WriteAllText(filePath, JsonConvert.SerializeObject(new ConnectionStringDto(), _jsonSettingsService.GetSettings()));
        }
    }

    public ConnectionStringDto ReadFromFile()
    {
        var filePath = ConnectionFilePath;
        if (!File.Exists(filePath))
        {
            throw new ConnectionFileNotFound(filePath);
        }

        var json = File.ReadAllText(filePath);
        return JsonConvert.DeserializeObject<ConnectionStringDto>(json, _jsonSettingsService.GetSettings()) ?? throw new JsonReadFailed(filePath);
    }

    public void SaveToFile(ConnectionStringDto connectionStringDto)
    {
        var connectionString = ReadFromFile();
        if (connectionString.ServerName is not null)
        {
            throw new ConnectionAlreadyExist();
        }
        var json = JsonConvert.SerializeObject(connectionStringDto, _jsonSettingsService.GetSettings());
        File.WriteAllText(ConnectionFilePath, json);
    }

    private string ConnectionFilePath => FilePathHelperService.GetDbSettingsFilePath();
}