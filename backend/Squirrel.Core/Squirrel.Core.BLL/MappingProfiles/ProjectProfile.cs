using AutoMapper;
using Squirrel.Core.Common.DTO.Projects;
using Squirrel.Core.Common.DTO.Sample;
using Squirrel.Core.DAL.Entities;

namespace Squirrel.Core.BLL.MappingProfiles;

public sealed class ProjectProfile : Profile
{
    public ProjectProfile()
    {
        CreateMap<Project, ProjectDto>();
        CreateMap<ProjectDto, Project>();
    }
}