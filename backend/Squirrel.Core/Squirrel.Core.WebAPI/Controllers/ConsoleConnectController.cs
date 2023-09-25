using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Squirrel.Core.BLL.Interfaces;
using Squirrel.Shared.DTO.ConsoleAppHub;

namespace Squirrel.Core.WebAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
[Authorize]
public class ConsoleConnectController : ControllerBase
{
    private readonly IConsoleConnectService _consoleConnectService;

    public ConsoleConnectController(IConsoleConnectService consoleConnectService)
    {
        _consoleConnectService = consoleConnectService;
    }
    [HttpPost("db-connect")]
    public async Task<ActionResult> ConnectToDb([FromBody] RemoteConnect remoteConnect)
    {
        await _consoleConnectService.TryConnect(remoteConnect);
        return Ok();
    }
}