using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Squirrel.Core.BLL.Interfaces;
using Squirrel.Core.Common.DTO.Users;

namespace Squirrel.Core.WebAPI.Controllers;

[Authorize]
[ApiController]
[Route("api/[controller]")]

public class UserController : ControllerBase
{
    private readonly IUserIdGetter _userIdGetter;
    private readonly IUserService _userService;

    public UserController(IUserIdGetter userIdGetter, IUserService userService)
    {
        _userIdGetter = userIdGetter;
        _userService = userService;
    }

    /// <summary>
    /// Update user names
    /// </summary>
    [HttpPut("update-names")]
    public async Task<ActionResult<UserDto>> UpdateUserNames([FromBody] UpdateUserNamesDTO updateUserDto)
    {
        updateUserDto.Id = _userIdGetter.GetCurrentUserId();
        return Ok(await _userService.UpdateUserAsync(updateUserDto));
    }

    /// <summary>
    /// Update user password
    /// </summary>
    [HttpPut("update-password")]
    public async Task<ActionResult> UpdatePassword([FromBody] UpdateUserPasswordDTO changePasswordDto)
    {
        changePasswordDto.Id = _userIdGetter.GetCurrentUserId();
        await _userService.ChangePasswordAsync(changePasswordDto);
        return NoContent();
    }

    /// <summary>
    /// Update user notifications
    /// </summary>
    [HttpPut("update-notifications")]
    public async Task<ActionResult<UserDto>> UpdateUserNotifications([FromBody] UpdateUserNotificationsdDTO updateUserNotificationsdDto)
    {
        updateUserNotificationsdDto.Id = _userIdGetter.GetCurrentUserId();
        return Ok(await _userService.UpdateNotificationsAsync(updateUserNotificationsdDto));
    }

    [HttpGet("fromToken")]
    public async Task<ActionResult<UserDto>> GetUserFromToken()
    {
        return Ok(await _userService.GetUserByIdAsync(_userIdGetter.GetCurrentUserId()));
    }
}
