using AutoMapper;
using Squirrel.Core.Common.DTO.ProjectDatabase;
using Squirrel.Core.DAL.Entities;

namespace Squirrel.Core.BLL.MappingProfiles;

public sealed class ProjectDatabaseProfile : Profile
{
    public ProjectDatabaseProfile()
    {
        CreateMap<ProjectDatabase, ProjectDatabaseDto>()!.ReverseMap();
        CreateMap<ProjectDatabase, DatabaseInfoDto>()!.ReverseMap();
    }
}