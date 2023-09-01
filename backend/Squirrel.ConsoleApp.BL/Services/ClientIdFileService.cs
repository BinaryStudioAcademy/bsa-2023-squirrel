using Newtonsoft.Json;
using Squirrel.ConsoleApp.BL.Exceptions;
using Squirrel.ConsoleApp.BL.Interfaces;
using Squirrel.ConsoleApp.Models.Models;

namespace Squirrel.ConsoleApp.BL.Services;

public class ClientIdFileService : IClientIdFileService
{
    private static string ClientIdFilePath
    {
        get
        {
            string userFolder = Environment.GetFolderPath(Environment.SpecialFolder.UserProfile);
            return Path.Combine(userFolder, "squirrel-client-id.json");
        }
    }


    public string GetClientId()
    {
        var filePath = ClientIdFilePath;
        if (!File.Exists(filePath))
        {
            return "";
        }

        var guid = File.ReadAllText(filePath);
        return guid;
    }

    public void SetClientId(string guid)
    {
        var filePath = ClientIdFilePath;
        File.WriteAllText(filePath, guid);
    }
}