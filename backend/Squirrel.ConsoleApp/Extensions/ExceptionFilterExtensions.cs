using System.Net;
using Squirrel.ConsoleApp.BL.Exceptions;
using Squirrel.ConsoleApp.Models.Models;

namespace Squirrel.ConsoleApp.Extensions;

public static class ExceptionFilterExtensions
{
    public static (HttpStatusCode statusCode, ErrorCode errorCode) ParseException(this Exception exception)
    {
        return exception switch
        {
            ConnectionFileNotFound _ => (HttpStatusCode.NotFound, ErrorCode.FileNotFound),
            JsonReadFailed _ => (HttpStatusCode.NotFound, ErrorCode.FileDamage),
            _ => (HttpStatusCode.InternalServerError, ErrorCode.General),
        };
    }
}