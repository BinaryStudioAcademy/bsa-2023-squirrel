using System.Net;
using Squirrel.ConsoleApp.Models;

namespace Squirrel.ConsoleApp.Extensions;

public static class ExceptionFilterExtensions
{
    public static (HttpStatusCode statusCode, ErrorCode errorCode) ParseException(this Exception exception)
    {
        return exception switch
        {
            FileNotFoundException _ => (HttpStatusCode.NotFound, ErrorCode.FileNotFound),
            InvalidOperationException _ => (HttpStatusCode.NotFound, ErrorCode.FileDamage),
            _ => (HttpStatusCode.InternalServerError, ErrorCode.General),
        };
    }
}