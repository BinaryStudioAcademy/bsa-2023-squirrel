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
        var currentUserId = _userIdGetter.GetCurrentUserId();

        var projectEntity = _mapper.Map<Project>(newProjectDto.Project);
        var currentUser = await _context.Users.FirstAsync(p => p.Id == currentUserId);
        projectEntity.Author = currentUser;
        projectEntity.CreatedBy = currentUserId;
        var createdProject = (await _context.Projects.AddAsync(projectEntity)).Entity;
        await _context.SaveChangesAsync();

        var defaultBranch = await _branchService.AddBranchAsync(createdProject.Id ,newProjectDto.DefaultBranch);
        createdProject.DefaultBranchId = defaultBranch.Id;
        await _context.SaveChangesAsync();

        return _mapper.Map<ProjectResponseDto>(createdProject);
    }

    public async Task<ProjectResponseDto> AddUsersToProjectAsync(int projectId, List<UserDto> usersDtos)
    {
        var existingProject = await _context.Projects.FindAsync(projectId);
        ValidateProject(existingProject);
        foreach (var user in usersDtos)
        {
            var userEntity = await _context.Users.FindAsync(user.Id);
            if (userEntity == null)
            {
                throw new EntityNotFoundException(nameof(User));
            }
            existingProject!.Users.Add(userEntity);
        }
        await _context.SaveChangesAsync();
        return _mapper.Map<ProjectResponseDto>(existingProject);
    }

    public async Task<ProjectResponseDto> UpdateProjectAsync(int projectId, UpdateProjectDto updateProjectDto)
    {
        var existingProject = await _context.Projects
            .Include(project => project.Users)
            .FirstOrDefaultAsync(project => project.Id == projectId);

        ValidateProject(existingProject);

        _mapper.Map(updateProjectDto, existingProject);

        await _context.SaveChangesAsync();

        return _mapper.Map<ProjectResponseDto>(existingProject)!;
    }

    public async Task<ProjectResponseDto> GetProjectAsync(int projectId)
    {
        var project = await _context.Projects
            .Include(project => project.Tags)
            .Include(project => project.Users)
            .FirstOrDefaultAsync(project => project.Id == projectId);
        var currentUserId = _userIdGetter.GetCurrentUserId();

        ValidateProject(project);

        var mappedProject = _mapper.Map<ProjectResponseDto>(project);
        
        mappedProject.IsAuthor = project!.CreatedBy == currentUserId;

        return mappedProject;
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
            .Include(p => p.Author)
            .FirstOrDefaultAsync(p => p.Id == projectId);

        ValidateProject(project);

        var projectUsers = project!.Users.ToList();
        projectUsers.Add(project.Author);

        return _mapper.Map<List<UserDto>>(projectUsers);
    }

    public async Task<List<ProjectResponseDto>> GetAllUserProjectsAsync()
    {
        var currentUserId = _userIdGetter.GetCurrentUserId();
        var userProjects = await _context.Projects
            .Include(p => p.Tags)
            .Where(p => p.CreatedBy == currentUserId ||
                        p.Users.Any(u => u.Id == currentUserId))
            .ToListAsync();

        return _mapper.Map<List<ProjectResponseDto>>(userProjects)!;
    }

    private void ValidateProject(Project? entity)
    {
        if (entity is null || (entity.Users.All(user => user.Id != _userIdGetter.GetCurrentUserId()) && entity.CreatedBy != _userIdGetter.GetCurrentUserId()))
        {
            throw new EntityNotFoundException(nameof(Project));
        }
    }
}