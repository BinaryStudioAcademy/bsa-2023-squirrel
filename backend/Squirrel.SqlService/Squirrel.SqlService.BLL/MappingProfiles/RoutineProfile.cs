using AutoMapper;
using Squirrel.ConsoleApp.Models;
using Squirrel.SqlService.BLL.Models.DTO.Shared;

namespace Squirrel.SqlService.BLL.MappingProfiles;

public sealed class RoutineProfile: Profile
{
    public RoutineProfile()
    {
        CreateMap<QueryResultTable, RoutineDefinitionDto>()
            .ForMember(dest => dest.Definition, opt
                => opt.MapFrom(src =>
                    src.Rows.Any()
                        ? src.Rows[0][0]
                        : string.Empty));
    }
}