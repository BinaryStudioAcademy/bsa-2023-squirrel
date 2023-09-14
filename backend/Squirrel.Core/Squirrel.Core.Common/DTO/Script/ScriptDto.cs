namespace Squirrel.Core.Common.DTO.Script;

public class ScriptDto : CreateScriptDto
{
    public int Id { get; set; }
    public string Content { get; set; } = null!;
}