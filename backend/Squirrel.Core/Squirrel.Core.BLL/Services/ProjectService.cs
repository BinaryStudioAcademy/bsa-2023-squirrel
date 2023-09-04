﻿using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Squirrel.Core.BLL.Interfaces;
using Squirrel.Core.BLL.Services.Abstract;
using Squirrel.Core.Common.DTO.Branch;
using Squirrel.Core.Common.DTO.Project;
using Squirrel.Core.DAL.Context;
using Squirrel.Core.DAL.Entities;
using Squirrel.Shared.Exceptions;

namespace Squirrel.Core.BLL.Services
{
    public sealed class ProjectService : BaseService, IProjectService
    {
        private readonly IBranchService _branchService;
        private readonly ICurrentUserIdService _userIdService;
        
        public ProjectService(SquirrelCoreContext context, IMapper mapper, IBranchService branchService, ICurrentUserIdService userIdService) : base(context, mapper)
        {
            _branchService = branchService;
            _userIdService = userIdService;
        }

        public async Task<ProjectDto> AddProjectAsync(ProjectDto projectDto)
        {
            var projectEntity = _mapper.Map<Project>(projectDto);
            projectEntity.CreatedBy = _userIdService.CurrentUserId;
            var createdProject = (await _context.Projects.AddAsync(projectEntity)).Entity;
            await _context.SaveChangesAsync();
            
            var defaultBranch = await _branchService.AddBranchAsync(new BranchDto
            {
                Name = projectDto.DefaultBranchName,
                IsActive = true,
                ProjectId = createdProject.Id
            });
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
            var currentUserId = _userIdService.CurrentUserId;
            var userProjects = await _context.Projects
                                             .Where(x => x.CreatedBy == currentUserId)
                                             .ToListAsync();

            return _mapper.Map<List<ProjectDto>>(userProjects)!;
        }
    }
}
