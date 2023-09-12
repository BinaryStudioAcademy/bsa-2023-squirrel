using Newtonsoft.Json;
using Squirrel.ConsoleApp.BL.Exceptions;
using Squirrel.ConsoleApp.BL.Interfaces;
using Squirrel.ConsoleApp.Models;

namespace Squirrel.ConsoleApp.BL.Services;

public class ConnectionFileService : IConnectionFileService
{
    private IJsonSerializerSettingsService _jsonSettingsService;

    public ConnectionFileService(IJsonSerializerSettingsService jsonSettingsService)
    {
        _jsonSettingsService = jsonSettingsService;
    }

    public void CreateInitFile()
    {
        var filePath = ConnectionFilePath;
        if (!File.Exists(filePath))
        {
            File.WriteAllText(filePath, JsonConvert.SerializeObject(new DbSettingsContainer(new DbSettings()), Formatting.Indented));
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
        return JsonConvert.DeserializeObject<ConnectionStringDto>(json) ?? throw new JsonReadFailed(filePath);
    }

    public void SaveToFile(ConnectionStringDto connectionStringDto)
    {
        var filePath = ConnectionFilePath;
        string json = JsonConvert.SerializeObject(connectionStringDto, Formatting.Indented);
        File.WriteAllText(filePath, json);
    }

    private string ConnectionFilePath
    {
        get
        {
            return FilePathHelperService.GetDbSettingsFilePath();
        }
    }
}