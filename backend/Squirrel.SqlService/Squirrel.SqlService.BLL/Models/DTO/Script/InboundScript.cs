using Squirrel.Core.DAL.Enums;

namespace Squirrel.SqlService.BLL.Models.DTO.Script;

public class InboundScriptDto
{
    public DbEngine DbEngine { get; set; }
    public string? InputSql { get; set; }
}
