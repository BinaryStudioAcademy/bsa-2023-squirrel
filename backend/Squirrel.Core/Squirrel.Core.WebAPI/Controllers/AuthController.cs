using Microsoft.AspNetCore.Mvc;
using Squirrel.Core.BLL.Interfaces;
using Squirrel.Core.Common.DTO.Auth;
using Squirrel.Core.Common.DTO.Users;

namespace Squirrel.Core.WebAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;

        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }

        [HttpPost("login/google")]
        [ProducesResponseType(typeof(AuthUserDTO), StatusCodes.Status200OK)]
        public async Task<ActionResult<AuthUserDTO>> LoginWithGoogle(GoogleToken dto)
        {
            return Ok(await _authService.AuthorizeWithGoogle(dto));
        }
    }
}
