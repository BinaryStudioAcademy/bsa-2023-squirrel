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
            var addedProject = await _projectService.AddProject(projectDto);
            
            return Ok(addedProject);
        }

        [HttpPut("{projectId}")]
        public async Task<ActionResult<ProjectDto>> UpdateProject(int projectId, ProjectDto projectDto)
        {
            var updatedProject = await _projectService.UpdateProject(projectId, projectDto);
            if (updatedProject == null)
                return NotFound();
            
            return Ok(updatedProject);
        }

        [HttpDelete("{projectId}")]
        public async Task<IActionResult> DeleteProject(int projectId)
        {
            var deletedProject = await _projectService.DeleteProject(projectId);
            if (deletedProject == null)
                return NotFound();

            return Ok(deletedProject);
        }

        [HttpGet("{projectId}")]
        public async Task<ActionResult<ProjectDto>> GetProject(int projectId)
        {
            var project = await _projectService.GetProject(projectId);
            if (project == null)
                return NotFound();
            
            return Ok(project);
        }

        [HttpGet]
        public async Task<ActionResult<List<ProjectDto>>> GetAllProjects()
        {
            var projects = await _projectService.GetAllProjects();
            return Ok(projects);
        }
    }
}
