using Squirrel.Core.BLL.Interfaces;

namespace Squirrel.Core.WebAPI.Middlewares;

public sealed class CurrentUserMiddleware
{
    private readonly RequestDelegate _next;

    public CurrentUserMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task InvokeAsync(HttpContext context, IUserIdSetter userIdSetter)
    {
        var claimsUserId = context.User.Claims.FirstOrDefault(x => x.Type == "id")?.Value;

        if (claimsUserId is not null && int.TryParse(claimsUserId, out var userId))
        {
            userIdSetter.SetCurrentUserId(userId);
        }

        await _next.Invoke(context);
    }
}
