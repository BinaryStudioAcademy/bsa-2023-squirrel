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
    public async Task<ActionResult<UserProfileDto>> UpdateUserNames([FromBody] UpdateUserNamesDto updateUserDto)
    {
        return Ok(await _userService.UpdateUserNamesAsync(updateUserDto));
    }

    /// <summary>
    /// Update user password
    /// </summary>
    [HttpPut("update-password")]
    public async Task<ActionResult> UpdatePassword([FromBody] UpdateUserPasswordDto changePasswordDto)
    {
        await _userService.ChangePasswordAsync(changePasswordDto);
        return NoContent();
    }

    /// <summary>
    /// Update user notifications
    /// </summary>
    [HttpPut("update-notifications")]
    public async Task<ActionResult<UserProfileDto>> UpdateUserNotifications([FromBody] UpdateUserNotificationsdDto updateUserNotificationsdDto)
    {
        return Ok(await _userService.UpdateNotificationsAsync(updateUserNotificationsdDto));
    }

    [HttpGet("fromToken")]
    public async Task<ActionResult<UserDto>> GetUserFromToken()
    {
        return Ok(await _userService.GetUserByIdAsync(_userIdGetter.GetCurrentUserId()));
    }

    [HttpGet("user-profile")]
    public async Task<ActionResult<UserProfileDto>> GetUserProfile()
    {
        return Ok(await _userService.GetUserProfileAsync());
    }
}
