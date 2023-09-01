using AutoMapper;
using Google.Apis.Auth;
using Squirrel.Core.Common.DTO.Auth;
using Squirrel.Core.DAL.Entities;

namespace Squirrel.Core.BLL.MappingProfiles;

public sealed class UserRegisterProfile : Profile
{
    public UserRegisterProfile()
    {
        CreateMap<User, UserRegisterDto>()!.ReverseMap();
        CreateMap<GoogleJsonWebSignature.Payload, UserRegisterDto>()
            .ForMember(m => m.FirstName, s => s.MapFrom(f => ReplaceSpaces(f.GivenName)))
            .ForMember(m => m.LastName, s => s.MapFrom(f => ReplaceSpaces(f.FamilyName)))
            .ForMember(m => m.Username, s => s.MapFrom(f => ReplaceSpaces(f.Name)));
    }

    private string ReplaceSpaces(string value) => value.Replace(' ', '_');
}