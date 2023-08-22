using Microsoft.AspNetCore.Mvc;
using Squirrel.Core.BLL.Services;
using Squirrel.Core.Common.DTO.Auth;
using Squirrel.Core.Common.DTO.Users;

namespace Squirrel.Core.WebAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly AuthService _authService;

        public AuthController(AuthService authService)
        {
            _authService = authService;
        }

        [HttpPost("login/google")]
        public async Task<ActionResult<AuthUserDTO>> Login(GoogleIdToken dto)
        {
            return Ok(await _authService.AuthorizeWithGoogle(dto));
        }
    }
}
