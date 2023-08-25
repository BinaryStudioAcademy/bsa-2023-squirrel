using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Squirrel.Core.Common.DTO.Projects;
using Squirrel.Core.BLL.Interfaces;
using Squirrel.Core.BLL.Services.Abstract;
using Squirrel.Core.Common.Enums;
using Squirrel.Core.DAL.Context;
using Squirrel.Core.DAL.Entities;

namespace Squirrel.Core.BLL.Services
{
    public class ProjectService : BaseService, IProjectService
    {
        private readonly SquirrelCoreContext _context;
        private readonly IMapper _mapper;

        public ProjectService(SquirrelCoreContext context, IMapper mapper): base(context, mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<ProjectDto> AddProject(ProjectDto projectDto)
        {
            var projectEntity = _mapper.Map<Project>(projectDto);
            
            projectEntity.Engine = (int)projectDto.Engine;
            
            _context.Projects.Add(projectEntity);
            
            await _context.SaveChangesAsync();
            
            return _mapper.Map<ProjectDto>(projectEntity);
        }

        public async Task<ProjectDto> UpdateProject(int projectId, ProjectDto projectDto)
        {
            var existingProject = await _context.Projects.FindAsync(projectId);

            if (existingProject == null)
                return _mapper.Map<ProjectDto>(existingProject);
            
            existingProject.Name = projectDto.Name; 
            existingProject.Engine = (int)projectDto.Engine;
            
            await _context.SaveChangesAsync();
            return _mapper.Map<ProjectDto>(existingProject);
        }

        public async Task<ProjectDto> DeleteProject(int projectId)
        {
            var project = await _context.Projects.FindAsync(projectId);
            if (project == null)
                return _mapper.Map<ProjectDto>(project);
                 
            _context.Projects.Remove(project);
            
            await _context.SaveChangesAsync();
            
            return _mapper.Map<ProjectDto>(project);
        }

        public async Task<ProjectDto> GetProject(int projectId)
        {
            var project = await _context.Projects.FindAsync(projectId);
            if (project == null)
                return _mapper.Map<ProjectDto>(project);
            
            var projectDto = _mapper.Map<ProjectDto>(project);
            projectDto.Engine = (EngineEnum)project.Engine;
            
            return projectDto;
        }

        public async Task<List<ProjectDto>> GetAllProjects()
        {
            var projects = await _context.Projects.ToListAsync();
            var projectDtos = _mapper.Map<List<ProjectDto>>(projects);
            
            foreach (var projectDto in projectDtos)
                projectDto.Engine = projectDto.Engine;

            return projectDtos;
        }
    }
}
