using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using Squirrel.Core.Common.Extensions;

namespace Squirrel.Core.WebAPI.Middlewares;

public class GenericExceptionHandlerMiddleware
{
    private readonly RequestDelegate _next;
    private readonly ILogger<GenericExceptionHandlerMiddleware> _logger;

    public GenericExceptionHandlerMiddleware(RequestDelegate next, ILogger<GenericExceptionHandlerMiddleware> logger)
    {
        _next = next;
        _logger = logger;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        try
        {
            await _next(context);
        }
        catch (Exception ex)
        {
            _logger.LogError($"Something went wrong: {ex}");
            await HandleException(context, ex);
        }
    }

    private static Task HandleException(HttpContext context, Exception exception)
    {
        var (errorDetails, statusCode) = exception.GetErrorDetailsAndStatusCode();
        
        context.Response.ContentType = "application/json";
        context.Response.StatusCode = (int)statusCode;

        return context.Response.WriteAsync(SerializeJson(errorDetails));
    }

    private static string SerializeJson<T>(T data)
    {
        var camelCasePropertiesSetting = new JsonSerializerSettings
        {
            ContractResolver = new CamelCasePropertyNamesContractResolver()
        };
        
        return JsonConvert.SerializeObject(data, camelCasePropertiesSetting);
    }
}