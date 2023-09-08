﻿using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Squirrel.ConsoleApp.BL.Interfaces;
using Squirrel.ConsoleApp.Models.DTO;

namespace Squirrel.ConsoleApp.Controllers;

[ApiController]
[Route("[controller]")]
public class TablesController: ControllerBase
{
    private readonly IMapper _mapper;
    private readonly IGetActionsService _getActionsService;

    public TablesController(IGetActionsService getActionsService, IMapper mapper)
    {
        _getActionsService = getActionsService;
        _mapper = mapper;
    }

    // http://localhost:44567/tables/get-names
    [HttpGet]
    [Route("get-names")]
    public async Task<ActionResult<TableNamesDto>> GetTablesNames()
    {
        var names = await _getActionsService.GetAllTablesNamesAsync();

        return Ok(_mapper.Map<TableNamesDto>(names));
    }

    // http://localhost:44567/tables/get-structure/dbo/categories
    [HttpGet]
    [Route("get-structure/{schema}/{name}")]
    public async Task<ActionResult<TableStructureDto>> GetTableStructure([FromRoute] string schema, string name)
    {
        var structure = await _getActionsService.GetTableStructureAsync(schema, name);

        return Ok(_mapper.Map<TableStructureDto>(structure));
    }

    // http://localhost:44567/tables/get-checks/dbo/employees
    [HttpGet]
    [Route("get-checks/{schema}/{name}")]
    public async Task<ActionResult<TableChecksDto>> GetTableChecks([FromRoute] string schema, string name)
    {
        var checks = await _getActionsService.GetTableChecksAndUniqueConstraintsAsync(schema, name);

        return Ok(_mapper.Map<TableChecksDto>(checks));
    }
}