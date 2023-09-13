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
    private readonly IImageService _imageService;

    public UserController(IUserIdGetter userIdGetter, IUserService userService, IImageService imageService)
    {
        _userIdGetter = userIdGetter;
        _userService = userService;
        _imageService = imageService;
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
    public async Task<ActionResult<UserProfileDto>> UpdateUserNotifications([FromBody] UpdateUserNotificationsDto updateUserNotificationsdDto)
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

    [HttpPost("add-avatar")]
    public async Task<ActionResult> AddUserAvatar(IFormFile avatar)
    {
        await _imageService.AddAvatarAsync(avatar);
        return NoContent();
    }
    
    [HttpDelete("delete-avatar")]
    public async Task<ActionResult> DeleteUserAvatar()
    {
        await _imageService.DeleteAvatarAsync();
        return NoContent();
    }
    
    [HttpGet("all")]
    public async Task<ActionResult<List<UserDto>>> GetAllUsers()
    {
        return Ok(await _userService.GetAllUsersAsync());
    }
}
