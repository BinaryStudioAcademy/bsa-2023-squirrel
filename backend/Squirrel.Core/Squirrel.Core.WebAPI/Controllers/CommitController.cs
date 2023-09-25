using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Squirrel.Core.BLL.Interfaces;
using Squirrel.Core.Common.DTO.Commit;

namespace Squirrel.Core.WebAPI.Controllers;
[Route("api/[controller]")]
[ApiController]
[Authorize]
public class CommitController : ControllerBase
{
    private readonly ICommitService _commitService;

    public CommitController(ICommitService commitService)
    {
        _commitService = commitService;
    }

    [HttpPost]
    public async Task<ActionResult<CommitDto>> CreateCommitAsync(CreateCommitDto dto)
    {
        return Ok(await _commitService.CreateCommitAsync(dto));
    }
}
