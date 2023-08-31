using Squirrel.Core.Common.DTO.Users;

namespace Squirrel.Core.BLL.Interfaces;

public interface IUserService
{
    Task<UserDTO> GetUserByIdAsync(int id);
    Task<UserDTO> UpdateUserAsync(UpdateUserNamesDTO updateUserDTO);
    Task ChangePasswordAsync(UpdateUserPasswordDTO userDto);
    Task<UserDTO> UpdateNotificationsAsync(UpdateUserNotificationsdDTO updateNotificationsdDTO);
}