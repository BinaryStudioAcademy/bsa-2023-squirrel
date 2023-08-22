using Squirrel.Core.Common.DTO.Auth;
using Squirrel.Core.Common.DTO.Users;

namespace Squirrel.Core.BLL.Interfaces
{
    public interface IAuthService
    {
        Task<AuthUserDTO> AuthorizeWithGoogle(GoogleToken googleToken);

    }
}
