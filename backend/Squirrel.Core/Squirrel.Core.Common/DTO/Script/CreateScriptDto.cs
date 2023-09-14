namespace Squirrel.Core.Common.DTO.Script;

public class CreateScriptDto
{
    public string Title { get; set; } = null!;
    public string FileName { get; set; } = null!;
    public int ProjectId { get; set; }
}