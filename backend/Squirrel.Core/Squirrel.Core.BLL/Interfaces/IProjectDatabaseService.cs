using Squirrel.Core.Common.DTO.ProjectDatabase;

namespace Squirrel.Core.BLL.Interfaces;

public interface IProjectDatabaseService
{
    Task<List<string>> GetAllProjectDbNamesAsync();
    Task<string> AddNewProjectDatabaseAsync(ProjectDatabaseDto dto);
}