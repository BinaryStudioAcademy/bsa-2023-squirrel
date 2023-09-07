using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Squirrel.Core.BLL.Interfaces;
using Squirrel.Core.Common.DTO.ProjectDatabase;

namespace Squirrel.Core.WebAPI.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize]
public sealed class ProjectDatabaseController : ControllerBase
{
    private readonly IProjectDatabaseService _projectDatabaseService;

    public ProjectDatabaseController(IProjectDatabaseService projectDatabaseService)
    {
        _projectDatabaseService = projectDatabaseService;
    }

    [HttpPost]
    public async Task<ActionResult<string>> AddNewProjectDatabase([FromBody] ProjectDatabaseDto databaseDto)
    {
        return Ok(await _projectDatabaseService.AddNewProjectDatabaseAsync(databaseDto));
    }

    [HttpGet("all")]
    public async Task<ActionResult<List<string>>> GetAllProjectDatabases()
    {
        return Ok(await _projectDatabaseService.GetAllProjectDbNamesAsync());
    }
}