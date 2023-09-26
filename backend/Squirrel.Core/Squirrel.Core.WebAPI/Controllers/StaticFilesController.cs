using Microsoft.AspNetCore.Mvc;
using OperatingSystem = Squirrel.Core.Common.Models.OperatingSystem;

namespace Squirrel.Core.WebAPI.Controllers;

[ApiController]
[Route("api/[controller]")]
public class StaticFilesController : Controller
{
    private readonly IConfiguration _configuration;

    public StaticFilesController(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    [HttpGet("squirrel-installer/{operatingSystem}"), DisableRequestSizeLimit]
    public async Task<IActionResult> DownloadSquirrelInstaller(OperatingSystem operatingSystem)
    {
        // path will be updated using 'operatingSystem' after task 152
        var filePath = _configuration["ConsoleSetupFilePath"];

        if (!System.IO.File.Exists(filePath))
        {
            return NotFound();
        }

        // fileDownloadName will be updated using 'operatingSystem' after task 152
        return File(
            await System.IO.File.ReadAllBytesAsync(filePath),
            "application/octet-stream",
            "SquirrelSetup.exe");
    }
}
