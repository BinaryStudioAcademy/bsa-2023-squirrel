using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Squirrel.Core.BLL.Interfaces;
using Squirrel.Core.BLL.Services.Abstract;
using Squirrel.Core.Common.DTO.Branch;
using Squirrel.Core.Common.DTO.Project;
using Squirrel.Core.DAL.Context;
using Squirrel.Core.DAL.Entities;
using Squirrel.Shared.Exceptions;

namespace Squirrel.Core.BLL.Services;

public sealed class ProjectService : BaseService, IProjectService
{
    private readonly IUserIdGetter _userIdGetter;
    private readonly IBranchService _branchService;
    
    public ProjectService(SquirrelCoreContext context, IMapper mapper, IUserIdGetter userIdGetter, IBranchService branchService)
        : base(context, mapper)
    {
        _userIdGetter = userIdGetter;
        _branchService = branchService;
    }

  
    public async Task<ProjectDto> AddProjectAsync(NewProjectDto newProjectDto)
    {
        var projectEntity = _mapper.Map<Project>(newProjectDto.Project);
        projectEntity.CreatedBy = _userIdGetter.GetCurrentUserId();
        var createdProject = (await _context.Projects.AddAsync(projectEntity)).Entity;
        await _context.SaveChangesAsync();

        newProjectDto.DefaultBranch.ProjectId = createdProject.Id;
        var defaultBranch = await _branchService.AddBranchAsync(newProjectDto.DefaultBranch);
        createdProject.DefaultBranchId = defaultBranch.Id;
        await _context.SaveChangesAsync();
        
        return _mapper.Map<ProjectDto>(createdProject);
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
        var currentUserId = _userIdGetter.GetCurrentUserId();
        var userProjects = await _context.Projects
                                         .Where(x => x.CreatedBy == currentUserId)
                                         .ToListAsync();

        return _mapper.Map<List<ProjectDto>>(userProjects)!;
    }
}
