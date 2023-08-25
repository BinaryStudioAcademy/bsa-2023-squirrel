using AutoMapper;
using Squirrel.Core.Common.DTO.Projects;
using Squirrel.Core.BLL.Interfaces;
using Squirrel.Core.BLL.Services.Abstract;
using Squirrel.Core.DAL.Context;

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

        public async Task<ProjectDTO> AddProject(ProjectDTO projectDto)
        {
            // var projectEntity = _mapper.Map<Project>(projectDto);
            // projectEntity.Engine = (int)projectDto.Engine;
            // _context.Projects.Add(projectEntity);
            // await _context.SaveChangesAsync();
            // return _mapper.Map<ProjectDTO>(projectEntity);

            throw new NotImplementedException();
        }

        public async Task<ProjectDTO> UpdateProject(int projectId, ProjectDTO projectDto)
        {
            // var existingProject = await _context.Projects.FindAsync(projectId);
            // if (existingProject == null)
            // {
            //     throw new NotFoundException(nameof(Project));
            // }
            
            // existingProject.Name = projectDto.Name;
            // existingProject.Engine = (int)projectDto.Engine;
            
            // await _context.SaveChangesAsync();
            // return _mapper.Map<ProjectDTO>(existingProject);

            throw new NotImplementedException();
        }

        public async Task DeleteProject(int projectId)
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

        public async Task<ProjectDTO> GetProject(int projectId)
        {
            // var project = await _context.Projects.FindAsync(projectId);
            // if (project == null)
            // {
            //     throw new NotFoundException(nameof(Project));
            // }
            // var projectDto = _mapper.Map<ProjectDTO>(project);
            // projectDto.Engine = (EngineEnum)project.Engine;
            // return projectDto;

            throw new NotImplementedException();
        }

        public async Task<List<ProjectDTO>> GetAllProjects()
        {
            // var projects = await _context.Projects.ToListAsync();
            // var projectDtos = _mapper.Map<List<ProjectDTO>>(projects);
            
            // foreach (var projectDto in projectDtos)
            // {
            //     projectDto.Engine = (EngineEnum)projectDto.Engine;
            // }

            // return projectDtos;

            throw new NotImplementedException();
        }
    }
}
