﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Squirrel.Core.BLL.Interfaces;
using Squirrel.Core.Common.DTO.Auth;

namespace Squirrel.Core.WebAPI.Controllers;

[ApiController]
[Route("api/[controller]")]
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
    ///     POST /api/auth/login
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
    ///     POST /api/auth/register
    ///     {
    ///        "email": "test@gmail.com",
    ///        "username": "username",
    ///        "firstName": "name",
    ///        "lastName": "surname",
    ///        "password": "myPassword1"
    ///     }
    ///
    /// </remarks>
    [HttpPost("register")]
    public async Task<ActionResult<RefreshedAccessTokenDto>> Post([FromBody] UserRegisterDto userRegisterData)
    {
        return Ok(await _authService.RegisterAsync(userRegisterData));
    }
}