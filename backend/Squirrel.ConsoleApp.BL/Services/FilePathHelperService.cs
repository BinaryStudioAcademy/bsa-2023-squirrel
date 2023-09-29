namespace Squirrel.ConsoleApp.BL.Services;

public static class FilePathHelperService
{
    private const string DbSettingsFileName = "squirrel-db-settings.json";
    private const string ClientIdFileName = "squirrel-client-id.json";
    
    public static string GetDbSettingsFilePath()
    {
        return Path.Combine(GetUserLocalDirectory(), DbSettingsFileName);
    }

    public static string GetClientIdFilePath()
    {
        return Path.Combine(GetUserLocalDirectory(), ClientIdFileName);
    }
    
    private static string GetUserLocalDirectory()
    {
        return Environment.GetFolderPath(Environment.SpecialFolder.UserProfile);
    }
}
