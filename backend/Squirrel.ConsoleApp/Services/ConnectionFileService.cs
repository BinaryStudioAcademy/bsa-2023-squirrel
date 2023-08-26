using Newtonsoft.Json;
using Squirrel.ConsoleApp.Interfaces;
using Squirrel.ConsoleApp.Models;

namespace Squirrel.ConsoleApp.Services;

public class ConnectionFileService: IConnectionFileService
{
    public void CreateEmptyFile()
    {
        var filePath = GetFilePath();
        File.WriteAllText(filePath, "{}");
    }

    public ConnectionString ReadFromFile()
    {
        var filePath = GetFilePath();
        if (File.Exists(filePath))
        {
            var json = File.ReadAllText(filePath);
            return JsonConvert.DeserializeObject<ConnectionString>(json) ?? throw new InvalidOperationException();
        }

        throw new FileNotFoundException(filePath);
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