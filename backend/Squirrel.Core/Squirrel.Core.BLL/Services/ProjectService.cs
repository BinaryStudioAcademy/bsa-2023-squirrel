using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Squirrel.Core.BLL.Interfaces;
using Squirrel.Core.BLL.Services.Abstract;
using Squirrel.Core.Common.DTO.Project;
using Squirrel.Core.DAL.Context;
using Squirrel.Core.DAL.Entities;
using Squirrel.Shared.Exceptions;

namespace Squirrel.Core.BLL.Services
{
    public sealed class ProjectService : BaseService, IProjectService
    {
        private readonly ICurrentUserIdService _currentUserIdService;
        
        public ProjectService(SquirrelCoreContext context, IMapper mapper, ICurrentUserIdService currentUserIdService) : base(context, mapper)
        {
            _currentUserIdService = currentUserIdService;
        }

        public async Task<ProjectDto> AddProjectAsync(ProjectDto projectDto)
        {
            var project = _mapper.Map<Project>(projectDto)!;
            var createdProject = (await _context.Projects.AddAsync(project)).Entity;
            
            await _context.SaveChangesAsync();
            
            return _mapper.Map<ProjectDto>(createdProject)!;
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
            
            return _mapper.Map<ProjectDto>(existingProject)!;
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
            
            return _mapper.Map<ProjectDto>(project)!;
        }

        public async Task<List<ProjectDto>> GetAllUserProjectsAsync()
        {
            var currentUserId = _currentUserIdService.CurrentUserId;
            var userProjects = await _context.Projects
                                             .Where(x => x.CreatedBy == currentUserId)
                                             .ToListAsync();

            return _mapper.Map<List<ProjectDto>>(userProjects)!;
        }
    }
}
