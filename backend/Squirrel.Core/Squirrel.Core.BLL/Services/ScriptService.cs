﻿using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Squirrel.Core.BLL.Interfaces;
using Squirrel.Core.BLL.Services.Abstract;
using Squirrel.Core.Common.DTO.Script;
using Squirrel.Core.DAL.Context;
using Squirrel.Core.DAL.Entities;
using Squirrel.Shared.Exceptions;

namespace Squirrel.Core.BLL.Services;

public sealed class ScriptService : BaseService, IScriptService
{
    public ScriptService(SquirrelCoreContext context, IMapper mapper) : base(context, mapper)
    {
    }

    public async Task<ScriptDto> CreateScriptAsync(CreateScriptDto dto, int authorId)
    {
        var script = _mapper.Map<Script>(dto);
        script.CreatedBy = script.LastUpdatedByUserId = authorId;
        var createdScript = (await _context.Scripts.AddAsync(script)).Entity;
        await _context.SaveChangesAsync();

        return _mapper.Map<ScriptDto>(createdScript);
    }

    public async Task<ScriptDto> UpdateScriptAsync(ScriptDto dto)
    {
        var script = await _context.Scripts.FindAsync(dto.Id);
        if (script is null)
        {
            throw new EntityNotFoundException();
        }

        _mapper.Map(dto, script);
        var updatedScript = _context.Scripts.Update(script).Entity;
        await _context.SaveChangesAsync();

        return _mapper.Map<ScriptDto>(updatedScript);
    }

    public async Task<List<ScriptDto>> GetAllScriptsAsync(int projectId)
    {
        var scripts = await _context.Scripts
                                    .Where(x => x.ProjectId == projectId)
                                    .ToListAsync();

        return _mapper.Map<List<ScriptDto>>(scripts);
    }
}