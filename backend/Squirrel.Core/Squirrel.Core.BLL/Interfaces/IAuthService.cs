using Squirrel.Core.Common.DTO.Auth;
using Squirrel.Core.Common.DTO.Users;

namespace Squirrel.Core.BLL.Interfaces;

public interface IAuthService
{
    Task<AuthUserDto> LoginAsync(UserLoginDto userLoginDto);
    Task<AuthUserDto> RegisterAsync(UserRegisterDto userRegisterDto);
    Task<AuthUserDto> AuthorizeWithGoogleAsync(string googleToken);
}