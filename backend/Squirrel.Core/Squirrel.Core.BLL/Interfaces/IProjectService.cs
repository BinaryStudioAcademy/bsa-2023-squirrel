using Squirrel.Core.Common.DTO.Projects;

namespace Squirrel.Core.BLL.Interfaces
{
    public interface IProjectService
    {
        Task<ProjectDto> AddProject(ProjectDto projectDto);
        Task<ProjectDto> UpdateProject(int projectId, ProjectDto projectDto);
        Task<ProjectDto> DeleteProject(int projectId);
        Task<ProjectDto> GetProject(int projectId);
        Task<List<ProjectDto>> GetAllProjects();
    }
}