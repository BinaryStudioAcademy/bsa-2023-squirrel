using AutoMapper;
using Google.Apis.Auth;
using Squirrel.Core.Common.DTO.Auth;

namespace Squirrel.Core.BLL.MappingProfiles;

public sealed class UserRegisterProfile : Profile
{
    public UserRegisterProfile()
    {
        CreateMap<GoogleJsonWebSignature.Payload, UserRegisterDto>()
            .ForMember(m => m.Email, s => s.MapFrom(f => f.Email))
            .ForMember(m => m.FirstName, s => s.MapFrom(f => f.GivenName))
            .ForMember(m => m.LastName, s => s.MapFrom(f => f.FamilyName))
            .ForMember(m => m.Username, s => s.MapFrom(f => f.Name));
    }
}