using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Squirrel.Core.BLL.Interfaces;
using Squirrel.Core.Common.DTO.Project;
using Squirrel.Core.Common.DTO.Users;

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
    
    [HttpPut("add-users/{projectId}")]
    public async Task<ActionResult<ProjectResponseDto>> AddUsersToProject(int projectId, [FromBody] List<UserDto> usersDtos)
    {
        return Ok(await _projectService.AddUsersToProjectAsync(projectId, usersDtos));
    }

    [HttpPut("{projectId}")]
    public async Task<ActionResult<ProjectResponseDto>> UpdateProject(int projectId, [FromBody] UpdateProjectDto updateProjectDto)
    {
        return Ok(await _projectService.UpdateProjectAsync(projectId, updateProjectDto));
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
    
    [HttpGet("team/{projectId}")]
    public async Task<ActionResult<List<UserDto>>> GetProjectUsers(int projectId)
    {
        return Ok(await _projectService.GetProjectUsersAsync(projectId));
    }

    [HttpGet("all")]
    public async Task<ActionResult<List<ProjectResponseDto>>> GetAllUserProjects()
    {
        return Ok(await _projectService.GetAllUserProjectsAsync());
    }
}