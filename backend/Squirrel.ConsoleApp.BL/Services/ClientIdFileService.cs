using Squirrel.ConsoleApp.BL.Interfaces;

namespace Squirrel.ConsoleApp.BL.Services;

public class ClientIdFileService : IClientIdFileService
{
    private static string ClientIdFilePath
    {
        get
        {
            return FilePathHelperService.GetClientIdFilePath();
        }
    }

    public Guid? GetClientId()
    {
        var filePath = ClientIdFilePath;
        if (!File.Exists(filePath))
        {
            return null!;
        }

        var guid = File.ReadAllText(filePath);
        return Guid.Parse(guid);
    }

    public void SetClientId(string guid)
    {
        File.WriteAllText(ClientIdFilePath, guid);
    }
}