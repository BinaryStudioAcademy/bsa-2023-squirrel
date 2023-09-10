using Microsoft.AspNetCore.Mvc;
using Squirrel.Core.BLL.Interfaces;

namespace Squirrel.Core.WebAPI.Controllers;

[ApiController]
[Route("api/[controller]")]
public class StaticFilesController : Controller
{
    private readonly IStaticFilesService _staticFilesService;

    public StaticFilesController(IStaticFilesService staticFilesService)
    {
        _staticFilesService = staticFilesService;
    }

    [HttpGet("downloadConsole"), DisableRequestSizeLimit]
    public async Task<IActionResult> DownloadConsole()
    {
        var memory = await _staticFilesService.GetConsoleSetupAsync();

        return File(memory, "application/octet-stream", "SquirrelSetup.msi");
    }
}
