using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Squirrel.Core.Common.DTO.Projects;
using Squirrel.Core.BLL.Interfaces;
using Squirrel.Core.BLL.Services.Abstract;
using Squirrel.Core.Common.Exceptions;
using Squirrel.Core.DAL.Context;
using Squirrel.Core.DAL.Entities;

namespace Squirrel.Core.BLL.Services
{
    public class ProjectService : BaseService, IProjectService
    {
        public ProjectService(SquirrelCoreContext context, IMapper mapper): base(context, mapper){ }

        public async Task<ProjectDto> AddProjectAsync(ProjectDto projectDto)
        {
            var projectEntity = _mapper.Map<Project>(projectDto);
            
            await _context.Projects.AddAsync(projectEntity);
            
            await _context.SaveChangesAsync();
            
            return _mapper.Map<ProjectDto>(projectEntity);
        }

        public async Task<ProjectDto> UpdateProjectAsync(int projectId, ProjectDto projectDto)
        {
            var existingProject = await _context.Projects.FindAsync(projectId);

            if (existingProject is null)
            {
                throw new EntityNotFoundException();
            }
            
            _mapper.Map(projectDto, existingProject);
            
            await _context.SaveChangesAsync();
            return _mapper.Map<ProjectDto>(existingProject);
        }

        public async Task DeleteProjectAsync(int projectId)
        {
            var project = await _context.Projects.FindAsync(projectId);
            if (project is null)
            {
                throw new EntityNotFoundException();
            }
    
            _context.Projects.Remove(project);
            await _context.SaveChangesAsync();
        }

        public async Task<ProjectDto> GetProjectAsync(int projectId)
        {
            var project = await _context.Projects.FindAsync(projectId);
            if (project is null)
            {
                throw new EntityNotFoundException();
            }            
            
            return _mapper.Map<ProjectDto>(project);;
        }

        public async Task<List<ProjectDto>> GetAllProjectsAsync()
        {
            var projects = await _context.Projects.ToListAsync();

            return _mapper.Map<List<ProjectDto>>(projects);
        }
    }
}
