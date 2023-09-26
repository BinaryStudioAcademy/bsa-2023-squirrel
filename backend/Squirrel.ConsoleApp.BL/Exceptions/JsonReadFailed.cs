namespace Squirrel.ConsoleApp.BL.Exceptions;

public class JsonReadFailed : Exception
{
    public JsonReadFailed(string path)
        : base($"Failed to read json from file with path: {path}")
    {
    }
}