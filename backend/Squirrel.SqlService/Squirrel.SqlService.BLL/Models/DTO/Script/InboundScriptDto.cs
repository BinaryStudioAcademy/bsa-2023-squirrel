using Squirrel.Core.DAL.Enums;

namespace Squirrel.SqlService.BLL.Models.DTO.Script;

public class InboundScriptDto
{
    public int ProjectId { get; set; }
    public DbEngine DbEngine { get; set; }
    public string Content { get; set; } = null!;
}
