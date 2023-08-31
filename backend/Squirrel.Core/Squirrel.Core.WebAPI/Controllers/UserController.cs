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
        public async Task<ActionResult<UserDTO>> GetById(int id)
        {
            return Ok(await _userService.GetUserByIdAsync(id));
        }

        /// <summary>
        /// Update user
        /// </summary>
        [HttpPut("update")]
        public async Task<ActionResult<UserDTO>> Put([FromBody] UpdateUserDTO user)
        {
            return Ok(await _userService.UpdateUserAsync(user));
        }
    }
}
