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
        context.Response.ContentType = "application/json";

        context.Response.StatusCode = exception switch
        {
            ArgumentNullException => 400,
            _ => 500
        };

        return context.Response.WriteAsync(exception.Message);
    }
}