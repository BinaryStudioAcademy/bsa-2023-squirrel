using System.Security.Claims;

namespace Squirrel.SqlService.WebApi.Middlewares;

public sealed class SignalRMiddleware
{
    private const string ClientIdClaimName = "ClientId";
    private readonly RequestDelegate _next;

    public SignalRMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        if (context.Request.Query.ContainsKey(ClientIdClaimName))
        {
            if (context.User?.Identity is not null)
            {
                var id = context.Request.Query[ClientIdClaimName];
                ((ClaimsIdentity)context.User.Identity).AddClaim(new Claim(ClientIdClaimName, id));
            }
        }

        await _next(context);
    }
}