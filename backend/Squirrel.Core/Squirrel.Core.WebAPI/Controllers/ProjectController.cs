﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Squirrel.Core.BLL.Interfaces;
using Squirrel.Core.Common.DTO.Project;

namespace Squirrel.Core.WebAPI.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize]
public class ProjectController : ControllerBase
{
    private readonly IProjectService _projectService;

    public ProjectController(IProjectService projectService)
    {
        _projectService = projectService;
    }
        
    /// <summary>
    /// Creates a new project with a default branch.
    /// </summary>
    /// <remarks>
    /// Example of a request to create a new project:
    ///
    ///     POST /api/project
    ///     {
    ///        "name": "projectName",
    ///        "defaultBranchName": "master",
    ///        "dbEngine": "2"
    ///     }
    /// </remarks>
    [HttpPost]
    public async Task<ActionResult<ProjectDto>> AddProject([FromBody] ProjectDto projectDto)
    {
        return Ok(await _projectService.AddProjectAsync(projectDto));
    }

    [HttpPut("{projectId}")]
    public async Task<ActionResult<ProjectDto>> UpdateProject(int projectId, ProjectDto projectDto)
    {
        return Ok(await _projectService.UpdateProjectAsync(projectId, projectDto));
    }

    [HttpDelete("{projectId}")]
    public async Task<IActionResult> DeleteProject(int projectId)
    {
        await _projectService.DeleteProjectAsync(projectId);
        return NoContent();
    }

    [HttpGet("{projectId}")]
    public async Task<ActionResult<ProjectDto>> GetProject(int projectId)
    {
        return Ok(await _projectService.GetProjectAsync(projectId));
    }

    [HttpGet("all")]
    public async Task<ActionResult<List<ProjectDto>>> GetAllUserProjects()
    {
        return Ok(await _projectService.GetAllUserProjectsAsync());
    }
}