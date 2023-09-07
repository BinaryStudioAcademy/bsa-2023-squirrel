using AutoMapper;
using Squirrel.Core.Common.DTO.Users;
using Squirrel.Core.DAL.Entities;

namespace Squirrel.Core.BLL.MappingProfiles;

public sealed class UserNamesProfile : Profile
{
    public UserNamesProfile()
    {
        CreateMap<User, UpdateUserNamesDto>()!.ReverseMap();
    }
}