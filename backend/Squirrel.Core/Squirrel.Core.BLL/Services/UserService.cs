﻿using AutoMapper;
using Squirrel.Core.BLL.Interfaces;
using Squirrel.Core.BLL.Services.Abstract;
using Squirrel.Core.DAL.Context;
using Squirrel.Core.DAL.Entities;
using Microsoft.EntityFrameworkCore;
using Squirrel.Core.Common.DTO.Users;
using Squirrel.Shared.Exceptions;
using Squirrel.Core.Common.Security;

namespace Squirrel.Core.BLL.Services;

public sealed class UserService : BaseService, IUserService
{
    public UserService(SquirrelCoreContext context, IMapper mapper) : base(context, mapper) { }

    public async Task<UserDTO> GetUserByIdAsync(int id)
    {
        var userEntity = await GetUserByIdInternal(id);
        return _mapper.Map<UserDTO>(userEntity);
    }

    public async Task<UserDTO> UpdateUserAsync(UpdateUserNamesDTO updateUserDTO)
    {
        var userEntity = await GetUserByIdInternal(updateUserDTO.Id);

        userEntity.Username = updateUserDTO.Username;
        userEntity.FirstName = updateUserDTO.FirstName;
        userEntity.LastName = updateUserDTO.LastName;

        _context.Users.Update(userEntity);
        await _context.SaveChangesAsync();

        return _mapper.Map<UserDTO>(userEntity);
    }

    public async Task ChangePasswordAsync(UpdateUserPasswordDTO changePasswordDTO)
    {
        var userEntity = await GetUserByIdInternal(changePasswordDTO.Id);

        var isPasswordValid = SecurityUtils.ValidatePassword(changePasswordDTO.CurrentPassword, userEntity.PasswordHash, userEntity.Salt);

        if (!isPasswordValid)
        {
            throw new InvalidEmailOrPasswordException();
        }

        var newPasswordHash = SecurityUtils.HashPassword(changePasswordDTO.NewPassword, userEntity.Salt);

        userEntity.PasswordHash = newPasswordHash;

        _context.Users.Update(userEntity);
        await _context.SaveChangesAsync();
    }

    public async Task<UserDTO> UpdateNotificationsAsync(UpdateUserNotificationsdDTO updateNotificationsdDTO)
    {
        var userEntity = await GetUserByIdInternal(updateNotificationsdDTO.Id);

        userEntity.SquirrelNotification = updateNotificationsdDTO.SquirrelNotification;
        userEntity.EmailNotification = updateNotificationsdDTO.EmailNotification;

        _context.Users.Update(userEntity);
        await _context.SaveChangesAsync();

        return _mapper.Map<UserDTO>(userEntity);
    }

    private async Task<User> GetUserByIdInternal(int id)
    {
        var userEntity = await _context.Users.FirstOrDefaultAsync(u => u.Id == id);

        if (userEntity == null)
        {
            throw new NotFoundException(nameof(User), id);
        }
        else
        {
            return userEntity;
        }
    }
}