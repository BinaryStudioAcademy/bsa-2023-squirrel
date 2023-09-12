using Squirrel.Core.Common.DTO.ProjectDatabase;

namespace Squirrel.Core.BLL.Interfaces;

public interface IProjectDatabaseService
{
    Task<List<DatabaseInfoDto>> GetAllProjectDbNamesAsync(int projectId);
    Task<DatabaseInfoDto> AddNewProjectDatabaseAsync(ProjectDatabaseDto dto);
}