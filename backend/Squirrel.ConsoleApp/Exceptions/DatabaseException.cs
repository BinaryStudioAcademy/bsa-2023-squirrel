namespace Squirrel.ConsoleApp.Exceptions
{
    public class DatabaseException : Exception
    {
        public DatabaseException(string? message) : base(message)
        {}
    }
}
