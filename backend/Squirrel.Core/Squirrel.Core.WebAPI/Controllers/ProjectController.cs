using Microsoft.AspNetCore.Mvc;
using Squirrel.Core.BLL.Services;
using Squirrel.Core.Common.DTO.Projects;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Squirrel.Core.WebAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ProjectController : ControllerBase
    {
        private readonly ProjectService _projectService;

        public ProjectController(ProjectService projectService)
        {
            _projectService = projectService;
        }

        [HttpPost]
        public async Task<ActionResult<ProjectDTO>> AddProject(ProjectDTO projectDto)
        {
            // TODO: Implement logic to add a project
            // var addedProject = await _projectService.AddProject(projectDto);
            // return Ok(addedProject);

            return BadRequest("Not implemented");
        }

        [HttpPut("{projectId}")]
        public async Task<ActionResult<ProjectDTO>> UpdateProject(Guid projectId, ProjectDTO projectDto)
        {
            // TODO: Implement logic to update a project by projectId
            // var updatedProject = await _projectService.UpdateProject(projectId, projectDto);
            // return Ok(updatedProject);

            return BadRequest("Not implemented");
        }

        [HttpDelete("{projectId}")]
        public async Task<IActionResult> DeleteProject(Guid projectId)
        {
            // TODO: Implement logic to delete a project by projectId
            // await _projectService.DeleteProject(projectId);
            // return NoContent();

            return BadRequest("Not implemented");
        }

        [HttpGet("{projectId}")]
        public async Task<ActionResult<ProjectDTO>> GetProject(Guid projectId)
        {
            // TODO: Implement logic to get a project by projectId
            // var project = await _projectService.GetProject(projectId);
            // return Ok(project);

            return BadRequest("Not implemented");
        }

        [HttpGet]
        public async Task<ActionResult<List<ProjectDTO>>> GetAllProjects()
        {
            // TODO: Implement logic to get all projects
            // var projects = await _projectService.GetAllProjects();
            // return Ok(projects);

            return BadRequest("Not implemented");
        }
    }
}
