using AutoMapper;
using Squirrel.Core.Common.DTO.Project;
using Squirrel.Core.DAL.Entities;

namespace Squirrel.Core.BLL.MappingProfiles;

public sealed class ProjectProfile : Profile
{
    public ProjectProfile()
    {
        CreateMap<Project, ProjectDto>();
        CreateMap<ProjectDto, Project>()
            .ForMember(d => d.DefaultBranch, o => o.Ignore());
    }
}