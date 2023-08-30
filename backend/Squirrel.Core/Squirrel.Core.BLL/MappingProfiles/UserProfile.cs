using AutoMapper;
using Squirrel.Core.Common.DTO.Auth;
using Squirrel.Core.Common.DTO.Users;
using Squirrel.Core.DAL.Entities;

namespace Squirrel.Core.BLL.MappingProfiles;

public sealed class UserProfile : Profile
{
    public UserProfile()
    {
        CreateMap<User, UserRegisterDto>()!.ReverseMap();
        CreateMap<User, UserDTO>()!.ReverseMap();
    }
}