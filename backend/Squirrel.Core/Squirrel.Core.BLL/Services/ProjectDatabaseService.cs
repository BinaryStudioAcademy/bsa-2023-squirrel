using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;
using Squirrel.Core.BLL.Interfaces;
using Squirrel.Core.BLL.Services.Abstract;
using Squirrel.Core.Common.DTO.ProjectDatabase;
using Squirrel.Core.DAL.Context;
using Squirrel.Core.DAL.Entities;
using Squirrel.Shared.Exceptions;

namespace Squirrel.Core.BLL.Services;

public sealed class ProjectDatabaseService : BaseService, IProjectDatabaseService
{
    public ProjectDatabaseService(SquirrelCoreContext context, IMapper mapper) : base(context, mapper)
    {
    }

    public async Task<ICollection<ProjectDatabaseDto>> GetAllProjectDbAsync(int projectId)
    {
        return await _context.ProjectDatabases
            .Where(p => p.ProjectId == projectId)
            .ProjectTo<ProjectDatabaseDto>(_mapper.ConfigurationProvider)
            .ToListAsync();
    }

    public async Task<DatabaseInfoDto> AddNewProjectDatabaseAsync(ProjectDatabaseDto dto)
    {
        var projectDb = _mapper.Map<ProjectDatabase>(dto);

        if (await _context.Projects.FindAsync(dto.ProjectId) is null)
        {
            throw new InvalidProjectException();
        }

        var addedProjectDb = (await _context.ProjectDatabases.AddAsync(projectDb)).Entity;
        await _context.SaveChangesAsync();

        return _mapper.Map<DatabaseInfoDto>(addedProjectDb);
    }
}