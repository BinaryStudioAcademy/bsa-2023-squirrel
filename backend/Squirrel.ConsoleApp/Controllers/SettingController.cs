﻿using Microsoft.AspNetCore.Mvc;
using Squirrel.ConsoleApp.BL.Interfaces;
using Squirrel.ConsoleApp.Models.Models;

namespace Squirrel.ConsoleApp.Controllers;

[ApiController]
[Route("[controller]")]
public class SettingController: ControllerBase
{
    private readonly IConnectionFileService _connectionFileService;

    public SettingController(IConnectionFileService connectionFileService)
    {
        _connectionFileService = connectionFileService;
    }
    
    [HttpPost]
    [Route("connect")]
    public IActionResult Post(ConnectionString connectionString)
    {
        Console.WriteLine(connectionString);
        _connectionFileService.SaveToFile(connectionString);

        var randomId = Guid.NewGuid();
        return Ok(randomId);
    }
}