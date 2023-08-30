using Squirrel.Core.Common.DTO.Auth;
using Squirrel.Core.Common.DTO.Users;

namespace Squirrel.Core.BLL.Interfaces;

public interface IAuthService
{
    Task<AuthUserDTO> LoginAsync(UserLoginDto userLoginDto);
    Task<AuthUserDTO> RegisterAsync(UserRegisterDto userRegisterDto);
    Task<AuthUserDTO> AuthorizeWithGoogleAsync(string googleToken);
}