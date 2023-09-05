using Squirrel.ConsoleApp.BL.Interfaces;

namespace Squirrel.ConsoleApp.BL.Services;

public class ClientIdFileService : IClientIdFileService
{
    private static string ClientIdFilePath
    {
        get
        {
            return HelperService.GetClientIdFilePath();
        }
    }


    public string GetClientId()
    {
        var filePath = ClientIdFilePath;
        if (!File.Exists(filePath))
        {
            return string.Empty;
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