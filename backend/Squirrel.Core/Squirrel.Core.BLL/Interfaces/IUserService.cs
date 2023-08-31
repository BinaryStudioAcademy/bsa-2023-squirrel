using Squirrel.Core.Common.DTO.Users;

namespace Squirrel.Core.BLL.Interfaces;

public interface IUserService
{
    Task<UserDTO> GetUserByIdAsync(int id);
    Task<UserDTO> UpdateUserAsync(UpdateUserDTO updateUserDTO);
    Task ChangePasswordAsync(ChangePasswordDTO userDto);
}