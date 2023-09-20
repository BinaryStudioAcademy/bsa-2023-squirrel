using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Squirrel.Shared.DTO.SelectedItems;
using Squirrel.SqlService.BLL.Interfaces;

namespace Squirrel.SqlService.WebApi.Controllers;
[Route("api/[controller]")]
[ApiController]
public class CommitFilesController : ControllerBase
{
    private readonly ICommitFilesService _commitFilesService;

    public CommitFilesController(ICommitFilesService commitFilesService)
    {
        _commitFilesService = commitFilesService;
    }

    [HttpPost]
    public async Task<ActionResult> Save(SelectedItemsDto dto)
    {
        await _commitFilesService.SaveSelectedFiles(dto);
        return Ok();
    }
}
