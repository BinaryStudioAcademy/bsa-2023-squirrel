using Squirrel.Core.Common.DTO.Project;
using Squirrel.Core.Common.DTO.Users;

namespace Squirrel.Core.BLL.Interfaces;

public interface IProjectService
{
    Task<ProjectResponseDto> AddProjectAsync(NewProjectDto newProjectDto);
    Task<ProjectResponseDto> UpdateProjectAsync(int projectId, UpdateProjectDto updateProjectDto);
    Task DeleteProjectAsync(int projectId);
    Task<ProjectResponseDto> GetProjectAsync(int projectId);
    Task<ProjectResponseDto> AddUsersToProjectAsync(int projectId, List<UserDto> usersDtos);
    Task<List<UserDto>> GetProjectUsersAsync(int projectId);
    Task<List<ProjectResponseDto>> GetAllUserProjectsAsync();
}