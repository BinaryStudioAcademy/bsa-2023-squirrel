using Squirrel.Core.Common.DTO.ProjectDatabase;

namespace Squirrel.Core.BLL.Interfaces;

public interface IProjectDatabaseService
{
    Task<ICollection<ProjectDatabaseDto>> GetAllProjectDbAsync(int projectId);
    Task<DatabaseInfoDto> AddNewProjectDatabaseAsync(ProjectDatabaseDto dto);
}