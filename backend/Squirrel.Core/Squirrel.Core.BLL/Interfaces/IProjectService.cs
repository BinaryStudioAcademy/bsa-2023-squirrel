using Squirrel.Core.Common.DTO.Projects;

namespace Squirrel.Core.BLL.Interfaces
{
    public interface IProjectService
    {
        Task<ProjectDTO> AddProject(ProjectDTO projectDto);
        Task<ProjectDTO> UpdateProject(int projectId, ProjectDTO projectDto);
        Task DeleteProject(int projectId);
        Task<ProjectDTO> GetProject(int projectId);
        Task<List<ProjectDTO>> GetAllProjects();
    }
}