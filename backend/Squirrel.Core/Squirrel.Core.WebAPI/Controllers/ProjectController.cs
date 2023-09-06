using Microsoft.AspNetCore.Authorization;
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

    [HttpPost]
    public async Task<ActionResult<ProjectResponseDto>> AddProject([FromBody] NewProjectDto newProjectDto)
    {
        return Ok(await _projectService.AddProjectAsync(newProjectDto));
    }

    [HttpPut("{projectId}")]
    public async Task<ActionResult<ProjectResponseDto>> UpdateProject(int projectId, ProjectDto projectDto)
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
    public async Task<ActionResult<ProjectResponseDto>> GetProject(int projectId)
    {
        return Ok(await _projectService.GetProjectAsync(projectId));
    }

    [HttpGet("all")]
    public async Task<ActionResult<List<ProjectResponseDto>>> GetAllUserProjects()
    {
        return Ok(await _projectService.GetAllUserProjectsAsync());
    }
}