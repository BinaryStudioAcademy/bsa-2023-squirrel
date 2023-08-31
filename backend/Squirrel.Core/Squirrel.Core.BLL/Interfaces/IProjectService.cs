using Squirrel.Core.Common.DTO.Project;

namespace Squirrel.Core.BLL.Interfaces
{
    public interface IProjectService
    {
        Task<ProjectDto> AddProjectAsync(ProjectDto projectDto);
        Task<ProjectDto> UpdateProjectAsync(int projectId, ProjectDto projectDto);
        Task DeleteProjectAsync(int projectId);
        Task<ProjectDto> GetProjectAsync(int projectId);
        Task<List<ProjectDto>> GetAllProjectsAsync();
    }
}