using AutoMapper;
using Squirrel.Core.Common.DTO.Users;
using Squirrel.Core.DAL.Entities;

namespace Squirrel.Core.BLL.MappingProfiles;

public sealed class UserProfile : Profile
{
    public UserProfile()
    {
        CreateMap<User, UserDto>()!.ReverseMap();
    }
}