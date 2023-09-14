using AutoMapper;
using Squirrel.Core.Common.DTO.Script;
using Squirrel.Core.DAL.Entities;

namespace Squirrel.Core.BLL.MappingProfiles;

public sealed class ScriptProfile : Profile
{
    public ScriptProfile()
    {
        CreateMap<Script, CreateScriptDto>()!.ReverseMap();
        CreateMap<Script, ScriptDto>()!.ReverseMap();
    }
}