using Newtonsoft.Json;
using Squirrel.ConsoleApp.BL.Exceptions;
using Squirrel.ConsoleApp.BL.Interfaces;
using Squirrel.ConsoleApp.Models;

namespace Squirrel.ConsoleApp.BL.Services;

public class ConnectionFileService : IConnectionFileService
{
    public void CreateEmptyFile()
    {
        var filePath = ConnectionFilePath;
        if (!File.Exists(filePath))
        {
            File.WriteAllText(filePath, JsonConvert.SerializeObject(new { DbSettings = new DbSettings() }, Formatting.Indented));
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
        return JsonConvert.DeserializeObject<DbSettings>(json) ?? throw new JsonReadFailed(filePath);
    }

    public void SaveToFile(DbSettings dbSettings)
    {
        var jsonSerializerSettings = new JsonSerializerSettings();
        jsonSerializerSettings.Converters.Add(new Newtonsoft.Json.Converters.StringEnumConverter());
        jsonSerializerSettings.Formatting = Formatting.Indented;

        string json = JsonConvert.SerializeObject(new { DbSettings = dbSettings }, jsonSerializerSettings);
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