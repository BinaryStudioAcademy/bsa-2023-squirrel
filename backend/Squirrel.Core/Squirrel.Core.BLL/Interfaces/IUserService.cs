﻿using Squirrel.Core.Common.DTO.Auth;
using Squirrel.Core.Common.DTO.Users;
using Squirrel.Core.DAL.Entities;

namespace Squirrel.Core.BLL.Interfaces;

public interface IUserService
{
    Task<UserDto> GetUserByIdAsync(int id);
    Task<UserDto> GetUserByEmailAsync(string email);
    Task<UserDto> GetUserByUsernameAsync(string username);
    Task<UserDto> CreateUserAsync(UserRegisterDto userDto, bool isGoogleAuth);
    Task<User?> GetUserEntityByEmail(string email);
    Task<User?> GetUserEntityByUsername(string username);

    Task<UserProfileDto> GetUserProfileAsync(int id);
    Task<UserProfileDto> UpdateUserNamesAsync(UpdateUserNamesDto updateUserDTO);
    Task ChangePasswordAsync(UpdateUserPasswordDto userDto);
    Task<UserProfileDto> UpdateNotificationsAsync(UpdateUserNotificationsdDto updateNotificationsdDTO);
}