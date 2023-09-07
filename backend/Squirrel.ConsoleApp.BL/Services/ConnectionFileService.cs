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


    public void CreateEmptyFile()
    {
        var filePath = ConnectionFilePath;
        if (!File.Exists(filePath))
        {
            File.WriteAllText(filePath, JsonConvert.SerializeObject(new DbSettingsContainer(new DbSettings()), Formatting.Indented));
        }
    }

    public DbSettings ReadFromFile()
    {
        var filePath = ConnectionFilePath;
        if (!File.Exists(filePath))
        {
            throw new ConnectionFileNotFound(filePath);
        }

        var json = File.ReadAllText(filePath);
        return JsonConvert.DeserializeObject<DbSettingsContainer>(json, _jsonSettingsService.GetSettings())?.DbSettings ?? throw new JsonReadFailed(filePath);
    }

    public void SaveToFile(DbSettings dbSettings)
    {
        string json = JsonConvert.SerializeObject(new DbSettingsContainer(dbSettings), _jsonSettingsService.GetSettings());
        File.WriteAllText(ConnectionFilePath, json);
    }

    private string ConnectionFilePath
    {
        get
        {
            return FilePathHelperService.GetDbSettingsFilePath();
        }
    }
}