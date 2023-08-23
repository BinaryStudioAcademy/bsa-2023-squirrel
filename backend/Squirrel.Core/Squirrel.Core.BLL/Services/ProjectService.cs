using AutoMapper;
using Squirrel.Core.Common.DTO.Projects;
using Squirrel.Core.DAL.Context;
using Squirrel.Core.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Squirrel.Core.BLL.Interfaces;

namespace Squirrel.Core.BLL.Services
{
    public class ProjectService: IProjectService
    {
        private readonly SquirrelCoreContext _context;
        private readonly IMapper _mapper;

        public ProjectService(SquirrelCoreContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<ProjectDTO> AddProject(ProjectDTO projectDto)
        {
            // var projectEntity = _mapper.Map<Project>(projectDto);
            // _context.Projects.Add(projectEntity);
            // await _context.SaveChangesAsync();
            // return _mapper.Map<ProjectDTO>(projectEntity);

            throw new NotImplementedException();
        }

        public async Task<ProjectDTO> UpdateProject(Guid projectId, ProjectDTO projectDto)
        {
            // var existingProject = await _context.Projects.FindAsync(projectId);
            // if (existingProject == null)
            // {
            //     throw new NotFoundException(nameof(Project));
            // }
            // _mapper.Map(projectDto, existingProject);
            // await _context.SaveChangesAsync();
            // return _mapper.Map<ProjectDTO>(existingProject);

            throw new NotImplementedException();
        }

        public async Task DeleteProject(Guid projectId)
        {
            // var project = await _context.Projects.FindAsync(projectId);
            // if (project == null)
            // {
            //     throw new NotFoundException(nameof(Project));
            // }
            // _context.Projects.Remove(project);
            // await _context.SaveChangesAsync();

            throw new NotImplementedException();
        }

        public async Task<ProjectDTO> GetProject(Guid projectId)
        {
            // var project = await _context.Projects.FindAsync(projectId);
            // if (project == null)
            // {
            //     throw new NotFoundException(nameof(Project));
            // }
            // return _mapper.Map<ProjectDTO>(project);

            throw new NotImplementedException();
        }

        public async Task<List<ProjectDTO>> GetAllProjects()
        {
            // var projects = await _context.Projects.ToListAsync();
            // return _mapper.Map<List<ProjectDTO>>(projects);

            throw new NotImplementedException();
        }
    }
}
