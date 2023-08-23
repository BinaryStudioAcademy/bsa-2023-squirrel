namespace Squirrel.Core.Common.DTO.Text;

public class CompareTextsDto
{
    public string OldText { get; set; }
    public string NewText { get; set; }
    public bool IgnoreWhitespace { get; set; } = false;
}
