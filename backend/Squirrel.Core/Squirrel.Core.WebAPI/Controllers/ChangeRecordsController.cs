using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Squirrel.Core.BLL.Interfaces;

namespace Squirrel.Core.WebAPI.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize]
public class ChangeRecordsController : ControllerBase
{
    private readonly IChangeRecordService _changeRecordService;

    public ChangeRecordsController(IChangeRecordService changeRecordService)
    {
        _changeRecordService = changeRecordService;
    }

    [HttpPost]
    public async Task<ActionResult<Guid>> AddChangeRecord()
    {
        return Ok(await _changeRecordService.AddChangeRecordAsync());
    }
}