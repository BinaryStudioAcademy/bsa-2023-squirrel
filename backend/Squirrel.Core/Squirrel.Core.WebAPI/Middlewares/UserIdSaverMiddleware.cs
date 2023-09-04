using Squirrel.Core.BLL.Interfaces;

namespace Squirrel.Core.WebAPI.Middlewares;

public sealed class UserIdSaverMiddleware
{
    private readonly RequestDelegate _next;

    public UserIdSaverMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task InvokeAsync(HttpContext context, ICurrentUserIdService userIdService)
    {
        userIdService.SetCurrentUserIdFromClaims(context.User.Claims);
        await _next(context);
    }
}