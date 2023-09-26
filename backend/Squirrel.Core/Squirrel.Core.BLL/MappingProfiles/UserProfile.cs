using AutoMapper;
using Squirrel.Core.BLL.MappingProfiles.MappingActions;
using Squirrel.Core.Common.DTO.Users;
using Squirrel.Core.DAL.Entities;

namespace Squirrel.Core.BLL.MappingProfiles;

public sealed class UserProfile : Profile
{
    public UserProfile()
    {
        CreateMap<User, UserDto>()!.AfterMap<BuildAvatarLinkAction>()!.ReverseMap();
        CreateMap<User, UserProfileDto>()!.AfterMap<BuildAvatarLinkAction>()!.ReverseMap();
    }
}