using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Squirrel.Core.BLL.Interfaces;
using Squirrel.Core.Common.DTO.Users;

namespace Squirrel.Core.WebAPI.Controllers;
[Route("api/[controller]")]
[ApiController]
[Authorize]
public class UserController : ControllerBase
{
    private readonly IUserIdGetter _userIdGetter;
    private readonly IUserService _userService;

    public UserController(IUserIdGetter userIdGetter, IUserService userService)
    {
        _userIdGetter = userIdGetter;
        _userService = userService;
    }

    [HttpGet("fromToken")]
    public async Task<ActionResult<UserDto>> GetUserFromToken()
    {
        return Ok(await _userService.GetUserByIdAsync(_userIdGetter.GetCurrentUserId()));
    }
}
