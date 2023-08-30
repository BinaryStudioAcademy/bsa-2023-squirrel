using AutoMapper;
using Squirrel.Core.BLL.Interfaces;
using Squirrel.Core.BLL.Services.Abstract;
using Squirrel.Core.DAL.Context;
using Squirrel.Core.DAL.Entities;
using Microsoft.EntityFrameworkCore;
using Squirrel.Core.Common.DTO.Users;
using Squirrel.Shared.Exceptions;

namespace Squirrel.Core.BLL.Services;

public sealed class UserService : BaseService, IUserService
{
    public UserService(SquirrelCoreContext context, IMapper mapper) : base(context, mapper) { }

    public async Task<UserDTO> GetUserByIdAsync(int id)
    {
        var userEntity = await GetUserByIdInternal(id);
        return _mapper.Map<UserDTO>(userEntity);
    }

    public async Task<UserDTO> UpdateUserAsync(UpdateUserDTO userDto)
    {
        var userEntity = await GetUserByIdInternal(userDto.Id);

        userEntity.Username = userDto.Username;
        userEntity.FirstName = userDto.FirstName;
        userEntity.LastName = userDto.LastName;

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