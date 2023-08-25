using Microsoft.AspNetCore.Mvc;
using Squirrel.Core.Common.DTO.Projects;
using Squirrel.Core.BLL.Interfaces;
using Squirrel.Core.Common.Enums;

namespace Squirrel.Core.WebAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProjectController : ControllerBase
    {
        private readonly IProjectService _projectService;

        public ProjectController(IProjectService projectService)
        {
            _projectService = projectService;
        }

        [HttpPost]
        public async Task<ActionResult<ProjectDto>> AddProject(ProjectDto projectDto)
        {
            var addedProject = await _projectService.AddProjectAsync(projectDto);
            
            return Ok(addedProject);
        }

        [HttpPut("{projectId}")]
        public async Task<ActionResult<ProjectDto>> UpdateProject(int projectId, ProjectDto projectDto)
        {
            var updatedProject = await _projectService.UpdateProjectAsync(projectId, projectDto);
            if (updatedProject == null)
            {
                return NotFound();
            }
            
            return Ok(updatedProject);
        }

        [HttpDelete("{projectId}")]
        public async Task<IActionResult> DeleteProject(int projectId)
        {
            var deletedProject = await _projectService.DeleteProjectAsync(projectId);
            if (deletedProject == null)
            {
                return NotFound();
            }
            
            return Ok(deletedProject);
        }

        [HttpGet("{projectId}")]
        public async Task<ActionResult<ProjectDto>> GetProject(int projectId)
        {
            var project = await _projectService.GetProjectAsync(projectId);
            if (project == null)
            {
                return NotFound();
            }
            
            return Ok(project);
        }

        [HttpGet]
        public async Task<ActionResult<List<ProjectDto>>> GetAllProjects()
        {
            var projects = await _projectService.GetAllProjectsAsync();
            return Ok(projects);
        }
    }
}
