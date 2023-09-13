using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Squirrel.Core.BLL.Interfaces;
using Squirrel.Core.Common.DTO.Auth;

namespace Squirrel.Core.WebAPI.Controllers;

[ApiController]
[Route("api/[controller]")]
[AllowAnonymous]
public sealed class TokenController : ControllerBase
{
    private readonly IAuthService _authService;

    public TokenController(IAuthService authService)
    {
        _authService = authService;
    }

    [HttpPost("refresh")]
    public async Task<ActionResult<RefreshedAccessTokenDto>> RefreshTokens(RefreshedAccessTokenDto tokens)
    {
        return Ok(await _authService.RefreshTokensAsync(tokens));
    }
}