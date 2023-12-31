﻿using AutoMapper;
using Squirrel.Core.Common.DTO.Project;
using Squirrel.Core.DAL.Entities;

namespace Squirrel.Core.BLL.MappingProfiles;

public sealed class ProjectProfile : Profile
{
    public ProjectProfile()
    {
        CreateMap<Project, ProjectDto>()!.ReverseMap();
        CreateMap<Project, UpdateProjectDto>()!.ReverseMap();
        CreateMap<Project, ProjectResponseDto>()!.ReverseMap();
    }
}