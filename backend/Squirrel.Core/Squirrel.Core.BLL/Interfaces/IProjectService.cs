﻿using Squirrel.Core.Common.DTO.Project;

namespace Squirrel.Core.BLL.Interfaces;

public interface IProjectService
{
    Task<ProjectDto> AddProjectAsync(NewProjectDto newProjectDto);
    Task<ProjectDto> UpdateProjectAsync(int projectId, UpdateProjectDto updateProjectDto);
    Task DeleteProjectAsync(int projectId);
    Task<ProjectDto> GetProjectAsync(int projectId);
    Task<List<ProjectDto>> GetAllUserProjectsAsync();
}