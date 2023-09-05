namespace Squirrel.ConsoleApp.BL.Services;

public static class HelperService
{
    public static string GetDbSettingsFilePath()
    {
        return Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.UserProfile), "squirrel-db-settings.json");
    }

    public static string GetClientIdFilePath()
    {
        return Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.UserProfile), "squirrel-client-id.json");
    }
}
