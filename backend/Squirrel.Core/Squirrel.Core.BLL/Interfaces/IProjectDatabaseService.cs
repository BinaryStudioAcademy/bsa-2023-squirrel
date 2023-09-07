using Squirrel.Core.Common.DTO.ProjectDatabase;

namespace Squirrel.Core.BLL.Interfaces;

public interface IProjectDatabaseService
{
    Task<List<ProjectInfoDto>> GetAllProjectDbNamesAsync();
    Task<ProjectInfoDto> AddNewProjectDatabaseAsync(ProjectDatabaseDto dto);
}