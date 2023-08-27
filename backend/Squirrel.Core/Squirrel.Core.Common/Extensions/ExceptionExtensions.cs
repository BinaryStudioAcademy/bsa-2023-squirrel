using System.Net;
using Squirrel.Core.Common.DTO.Error;
using Squirrel.Core.Common.Enums;
using Squirrel.Core.Common.Exceptions.Abstract;

namespace Squirrel.Core.Common.Extensions;

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