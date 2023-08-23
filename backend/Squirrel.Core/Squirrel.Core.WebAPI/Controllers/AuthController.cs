using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Squirrel.Core.BLL.Interfaces;
using Squirrel.Core.Common.DTO.Auth;
using Squirrel.Core.Common.DTO.Users;

namespace Squirrel.Core.WebAPI.Controllers;

[ApiController]
[Route("[controller]")]
[AllowAnonymous]
public sealed class AuthController : ControllerBase
{
    private readonly IAuthService _authService;

    public AuthController(IAuthService authService)
    {
        _authService = authService;
    }

    /// <summary>
    /// Generates user token for login
    /// </summary>
    /// <remarks>
    /// Sample request for login:
    ///
    ///     POST /auth/login
    ///     {
    ///        "email": "test@gmail.com",
    ///        "password": "myPassword1"
    ///     }
    ///
    /// </remarks>
    [HttpPost("login")]
    public async Task<ActionResult<RefreshedAccessTokenDto>> Login([FromBody] UserLoginDto userLoginData)
    {
        return Ok(await _authService.LoginAsync(userLoginData));
    }

    /// <summary>
    /// Registers new user and generates tokens
    /// </summary>
    /// <remarks>
    /// Sample request for registration:
    ///
    ///     POST /auth/register
    ///     {
    ///        "email": "test@gmail.com",
    ///        "username": "username",
    ///        "password": "myPassword1",
    ///     }
    ///
    /// </remarks>
    [HttpPost("register")]
    public async Task<ActionResult<RefreshedAccessTokenDto>> Post([FromBody] UserRegisterDto userRegisterData)
    {
        // TODO: Create user and add him to database via UserService.
        return Ok(await _authService.RegisterAsync(userRegisterData));
    }

    /// <summary>
    /// Generates user token for login with google tokenId
    /// </summary>
    /// <remarks>
    /// Sample request for login:
    ///
    ///     POST /auth/login/google
    ///     {
    ///        "idToken": "token",
    ///     }
    ///
    /// </remarks>
    [HttpPost("login/google")]
    [ProducesResponseType(typeof(AuthUserDTO), StatusCodes.Status200OK)]
    public async Task<ActionResult<AuthUserDTO>> LoginWithGoogle(GoogleToken googleTokenData)
    {
        return Ok(await _authService.AuthorizeWithGoogleAsync(googleTokenData));
    }
}