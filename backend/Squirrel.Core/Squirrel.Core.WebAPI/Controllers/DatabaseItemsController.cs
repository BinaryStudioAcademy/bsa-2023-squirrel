using Microsoft.AspNetCore.Mvc;
using Squirrel.Core.BLL.Interfaces;
using Squirrel.Shared.DTO.DatabaseItem;

namespace Squirrel.Core.WebAPI.Controllers;

[ApiController]
[Route("api/[controller]")]
public class DatabaseItemsController : ControllerBase
{
    private readonly IHttpInternalService _httpInternalService;
    private readonly IConfiguration _configuration;

    public DatabaseItemsController(IHttpInternalService httpInternalService, IConfiguration configuration)
    {
        _httpInternalService = httpInternalService;
        _configuration = configuration;
    }

    [HttpGet]
    public async Task<ActionResult<List<DatabaseItem>>> GetAllItems()
    {
        return Ok(await _httpInternalService.GetAsync<List<DatabaseItem>>($"{_configuration.GetValue<string>("SqlServiceUrl")}/api/DatabaseItems"));
    }
}

