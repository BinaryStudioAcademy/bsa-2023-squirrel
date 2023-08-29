using System.Net;
using Squirrel.ExceptionHandling.DTO.Error;
using Squirrel.ExceptionHandling.Enums;
using Squirrel.ExceptionHandling.Exceptions.Abstract;

namespace Squirrel.ExceptionHandling.Extensions;

public static class ExceptionExtensions
{
    public static (ErrorDetailsDto, HttpStatusCode) GetErrorDetailsAndStatusCode(this Exception exception)
    {
        return exception switch
        {
            RequestException e => (new ErrorDetailsDto(e.Message, e.ErrorType), e.StatusCode),
            _ => (new ErrorDetailsDto(exception.Message, ErrorType.Internal), HttpStatusCode.InternalServerError)
        };
    }
}