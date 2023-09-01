namespace Squirrel.ConsoleApp.BL.Exceptions;

public class ForbiddenToChangeClientId : Exception
{
    public ForbiddenToChangeClientId(string path) 
        : base($"Client ID file with path: {path} is already exist! You can't change Client ID!") { }
}