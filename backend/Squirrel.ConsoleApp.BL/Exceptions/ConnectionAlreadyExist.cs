namespace Squirrel.ConsoleApp.BL.Exceptions;

public class ConnectionAlreadyExist: Exception
{
    public ConnectionAlreadyExist() 
        : base($"Console already has connection to db, try another one or delete current db") { }
}