using Microsoft.AspNetCore.Mvc;
using Squirrel.Core.BLL.Interfaces;

namespace Squirrel.Core.WebAPI.Controllers;

[ApiController]
[Route("api/[controller]")]
public sealed class TokenController : ControllerBase
{
    private IAuthService _authService;

    public TokenController(IAuthService authService)
    {
        _authService = authService;
    }

    [HttpGet("id")]
    public int GetUserIdFromToken()
    {
        return _authService.GetUserIdFromToken(HttpContext.User.Claims);
    }
}