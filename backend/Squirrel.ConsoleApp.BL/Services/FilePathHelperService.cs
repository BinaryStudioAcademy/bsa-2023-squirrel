namespace Squirrel.ConsoleApp.BL.Services;

public static class FilePathHelperService
{
    private static string GetUserLocalDirectory()
    {
        return Environment.GetFolderPath(Environment.SpecialFolder.UserProfile);
    }
    
    public static string GetDbSettingsFilePath()
    {
        return Path.Combine(GetUserLocalDirectory(), "squirrel-db-settings.json");
    }

    public static string GetClientIdFilePath()
    {
        return Path.Combine(GetUserLocalDirectory(), "squirrel-client-id.json");
    }
}
