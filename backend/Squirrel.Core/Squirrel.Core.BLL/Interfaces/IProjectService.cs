using Squirrel.Core.Common.DTO.Projects;

namespace Squirrel.Core.BLL.Interfaces
{
    public interface IProjectService
    {
        Task<ProjectDTO> AddProject(ProjectDTO projectDto);
        Task<ProjectDTO> UpdateProject(Guid projectId, ProjectDTO projectDto);
        Task DeleteProject(Guid projectId);
        Task<ProjectDTO> GetProject(Guid projectId);
        Task<List<ProjectDTO>> GetAllProjects();
    }
}