using AutoMapper;
using Squirrel.Core.Common.DTO.Tag;
using Squirrel.Core.DAL.Entities;

namespace Squirrel.Core.BLL.MappingProfiles;

public sealed class TagProfile : Profile
{
    public TagProfile()
    {
        CreateMap<Tag, TagDto>()!.ReverseMap();
    }
}