using System.Security.Claims;

namespace Squirrel.SqlService.WebApi.Middlewares;

public class SignalRMiddleware
{

    private readonly RequestDelegate _next;

    public SignalRMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        if (context.Request.Query.ContainsKey("ClientId"))
        {
            if (context.User?.Identity is not null)
            {
                var id = context.Request.Query["ClientId"];
                ((ClaimsIdentity)context.User.Identity).AddClaim(new Claim("ClientId", id));
            }
        }

        await _next(context);
    }
}