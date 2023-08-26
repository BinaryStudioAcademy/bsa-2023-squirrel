using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Squirrel.ConsoleApp.Extensions;

namespace Squirrel.ConsoleApp.Filters;

public class CustomExceptionFilter
{
    public void OnException(ExceptionContext context)
    {
        var exception = context.Exception;
        var (statusCode, errorCode) = exception.ParseException();
        var response = new ObjectResult(exception.Message)
        {
            StatusCode = (int)statusCode
        };
        
        context.Result = response;

        context.ExceptionHandled = true;
    }
}