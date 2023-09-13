using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Squirrel.Core.BLL.Interfaces;
using Squirrel.Core.BLL.Services.Abstract;
using Squirrel.Core.Common.DTO.Project;
using Squirrel.Core.Common.DTO.Users;
using Squirrel.Core.DAL.Context;
using Squirrel.Core.DAL.Entities;
using Squirrel.Shared.Exceptions;

namespace Squirrel.Core.BLL.Services;

public sealed class ProjectService : BaseService, IProjectService
{
    private readonly IUserIdGetter _userIdGetter;
    private readonly IBranchService _branchService;

    public ProjectService(SquirrelCoreContext context, IMapper mapper, IUserIdGetter userIdGetter,
        IBranchService branchService)
        : base(context, mapper)
    {
        _userIdGetter = userIdGetter;
        _branchService = branchService;
    }


    public async Task<ProjectResponseDto> AddProjectAsync(NewProjectDto newProjectDto)
    {
        var projectEntity = _mapper.Map<Project>(newProjectDto.Project);
        projectEntity.CreatedBy = _userIdGetter.GetCurrentUserId();
        var createdProject = (await _context.Projects.AddAsync(projectEntity)).Entity;
        await _context.SaveChangesAsync();

        newProjectDto.DefaultBranch.ProjectId = createdProject.Id;
        var defaultBranch = await _branchService.AddBranchAsync(newProjectDto.DefaultBranch);
        createdProject.DefaultBranchId = defaultBranch.Id;
        await _context.SaveChangesAsync();

        return _mapper.Map<ProjectResponseDto>(createdProject);
    }

    public async Task<ProjectResponseDto> AddUsersToProjectAsync(int projectId, List<UserDto> usersDtos)
    {
        var users = _mapper.Map<List<User>>(usersDtos);

        var existingProject = await _context.Projects.FindAsync(projectId);

        ValidateProject(existingProject);

        foreach (var user in users)
        {
            existingProject!.Users.Add(user);
        }

        await _context.SaveChangesAsync();
        return _mapper.Map<ProjectResponseDto>(existingProject);
    }

    public async Task<ProjectResponseDto> UpdateProjectAsync(int projectId, UpdateProjectDto updateProjectDto)
    {
        var existingProject = await _context.Projects.FindAsync(projectId);

        ValidateProject(existingProject);

        _mapper.Map(updateProjectDto, existingProject);

        await _context.SaveChangesAsync();

        return _mapper.Map<ProjectResponseDto>(existingProject)!;
    }

    public async Task<ProjectResponseDto> GetProjectAsync(int projectId)
    {
        var project = await _context.Projects
            .Include(project => project.Tags)
            .FirstOrDefaultAsync(project => project.Id == projectId);

        ValidateProject(project);

        return _mapper.Map<ProjectResponseDto>(project);
    }

    public async Task DeleteProjectAsync(int projectId)
    {
        var project = await _context.Projects.FindAsync(projectId);

        ValidateProject(project);

        _context.Projects.Remove(project!);
        await _context.SaveChangesAsync();
    }

    public async Task<List<UserDto>> GetProjectUsersAsync(int projectId)
    {
        var project = await _context.Projects
            .Include(p => p.Users)
            .FirstOrDefaultAsync(p => p.Id == projectId);

        ValidateProject(project);

        var projectUsers = project!.Users.ToList();

        return _mapper.Map<List<UserDto>>(projectUsers);
    }

    public async Task<List<ProjectResponseDto>> GetAllUserProjectsAsync()
    {
        var currentUserId = _userIdGetter.GetCurrentUserId();
        var userProjects = await _context.Projects
            .Include(project => project.Tags)
            .Where(x => x.CreatedBy == currentUserId)
            .ToListAsync();

        return _mapper.Map<List<ProjectResponseDto>>(userProjects)!;
    }

    private void ValidateProject(Project? entity)
    {
        if (entity is null || entity.CreatedBy != _userIdGetter.GetCurrentUserId())
        {
            throw new EntityNotFoundException(nameof(Project));
        }
    }
}