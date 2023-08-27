using Newtonsoft.Json;
using Squirrel.ConsoleApp.BL.Exceptions;
using Squirrel.ConsoleApp.BL.Interfaces;
using Squirrel.ConsoleApp.Models.Models;

namespace Squirrel.ConsoleApp.BL.Services;

public class ConnectionFileService : IConnectionFileService
{
    public void CreateEmptyFile()
    {
        var filePath = GetFilePath();
        if (!File.Exists(filePath))
        {
            File.WriteAllText(filePath, "{}");
        }
    }

    public ConnectionString ReadFromFile()
    {
        var filePath = GetFilePath();
        if (!File.Exists(filePath))
        {
            throw new ConnectionFileNotFound(filePath);
        }
        
        var json = File.ReadAllText(filePath);
        return JsonConvert.DeserializeObject<ConnectionString>(json) ?? throw new JsonReadFailed(filePath);
    }

    public void SaveToFile(ConnectionString connectionString)
    {
        var filePath = GetFilePath();
        string json = JsonConvert.SerializeObject(connectionString, Formatting.Indented);
        File.WriteAllText(filePath, json);
    }

    private string GetFilePath()
    {
        string userFolder = Environment.GetFolderPath(Environment.SpecialFolder.UserProfile);
        return Path.Combine(userFolder, "squirrel-connection.json");
    }
}