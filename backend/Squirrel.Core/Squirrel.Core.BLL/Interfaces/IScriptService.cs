using Squirrel.ConsoleApp.Models;
using Squirrel.Core.Common.DTO.Script;

namespace Squirrel.Core.BLL.Interfaces;

public interface IScriptService
{
    Task<ScriptDto> CreateScriptAsync(CreateScriptDto dto, int authorId);
    Task<ScriptDto> UpdateScriptAsync(ScriptDto dto, int editorId);
    Task<ICollection<ScriptDto>> GetAllScriptsAsync(int projectId);
    Task<ScriptContentDto> GetFormattedSqlAsync(InboundScriptDto inboundScriptDto);
    Task<QueryResultTable> ExecuteSqlScriptAsync(InboundScriptDto inboundScriptDto);
}