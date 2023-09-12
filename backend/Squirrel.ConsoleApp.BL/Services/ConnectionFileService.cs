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
            File.WriteAllText(filePath, "{}");
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
            string userFolder = Environment.GetFolderPath(Environment.SpecialFolder.UserProfile);
            return Path.Combine(userFolder, "squirrel-connection.json");
        }
    }
}