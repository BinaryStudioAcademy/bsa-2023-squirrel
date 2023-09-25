namespace Squirrel.ConsoleApp.BL.Exceptions;

public class ConnectionFileNotFound : Exception
{
    public ConnectionFileNotFound(string path)
        : base($"Connection File with path: {path} was not found")
    {
    }
}