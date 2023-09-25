using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Squirrel.ConsoleApp.Models;
using Squirrel.Core.BLL.Interfaces;
using Squirrel.Core.BLL.Services.Abstract;
using Squirrel.Core.Common.DTO.Script;
using Squirrel.Core.DAL.Context;
using Squirrel.Core.DAL.Entities;
using Squirrel.Shared.Exceptions;

namespace Squirrel.Core.BLL.Services;

public sealed class ScriptService : BaseService, IScriptService
{
    private readonly IConfiguration _configuration;
    private readonly IHttpClientService _httpClientService;

    public ScriptService(SquirrelCoreContext context, IMapper mapper, IHttpClientService httpClientService, IConfiguration configuration) : base(context, mapper)
    {
        _httpClientService = httpClientService;
        _configuration = configuration;
    }

    public async Task<ScriptDto> CreateScriptAsync(CreateScriptDto dto, int authorId)
    {
        var script = _mapper.Map<Script>(dto);
        script.CreatedBy = script.LastUpdatedByUserId = authorId;
        var createdScript = (await _context.Scripts.AddAsync(script)).Entity;
        await _context.SaveChangesAsync();

        return _mapper.Map<ScriptDto>(createdScript);
    }

    public async Task<ScriptDto> UpdateScriptAsync(ScriptDto dto, int editorId)
    {
        var script = await _context.Scripts.FindAsync(dto.Id);
        if (script is null)
        {
            throw new EntityNotFoundException();
        }

        _mapper.Map(dto, script);
        script.LastUpdatedByUserId = editorId;
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

    public async Task<ScriptContentDto> GetFormattedSqlAsync(InboundScriptDto inboundScriptDto)
    {
        return await _httpClientService.SendAsync<InboundScriptDto, ScriptContentDto>
            ($"{_configuration["SqlServiceUrl"]}/api/Script/format", inboundScriptDto, HttpMethod.Put);
    }

    public async Task<QueryResultTable> ExecuteSqlScriptAsync(InboundScriptDto inboundScriptDto)
    {
        return await _httpClientService.SendAsync<InboundScriptDto, QueryResultTable>
           ($"{_configuration["SqlServiceUrl"]}/api/ConsoleAppHub/execute-script", inboundScriptDto, HttpMethod.Post);
    }
}