using Squirrel.Core.Common.DTO.Project;

namespace Squirrel.Core.BLL.Interfaces;

public interface IProjectService
{
    Task<ProjectResponseDto> AddProjectAsync(NewProjectDto newProjectDto);
    Task<ProjectResponseDto> UpdateProjectAsync(int projectId, ProjectDto projectDto);
    Task DeleteProjectAsync(int projectId);
    Task<ProjectResponseDto> GetProjectAsync(int projectId);
    Task<List<ProjectResponseDto>> GetAllUserProjectsAsync();
}