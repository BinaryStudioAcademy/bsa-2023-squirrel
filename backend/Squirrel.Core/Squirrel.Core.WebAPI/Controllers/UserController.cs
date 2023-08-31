using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Squirrel.Core.BLL.Interfaces;
using Squirrel.Core.Common.DTO.Users;

namespace Squirrel.Core.WebAPI.Controllers
{
    [ApiController]
    //[Authorize]
    [Route("api/[controller]")]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        /// <summary>
        /// Get user information by ID
        /// </summary>
        [HttpGet("{id}")]
        public async Task<ActionResult<UserDTO>> GetUserById(int id)
        {
            return Ok(await _userService.GetUserByIdAsync(id));
        }

        /// <summary>
        /// Update user names
        /// </summary>
        [HttpPut("update-names")]
        public async Task<ActionResult<UserDTO>> UpdateUserNames([FromBody] UpdateUserDTO updateUserDTO)
        {
            return Ok(await _userService.UpdateUserAsync(updateUserDTO));
        }

        /// <summary>
        /// Update user notifications
        /// </summary>
        [HttpPut("update-notifications")]
        public async Task<ActionResult<UserDTO>> UpdateUserNotifications([FromBody] UpdateUserDTO updateUserDTO)
        {
            return Ok(await _userService.UpdateUserAsync(updateUserDTO));
        }

        /// <summary>
        /// Update user password
        /// </summary>
        [HttpPut("change-password")]
        public async Task<ActionResult> UpdatePassword([FromBody] ChangePasswordDTO changePassword)
        {
            await _userService.ChangePasswordAsync(changePassword);
            return NoContent();
        }
    }
}
