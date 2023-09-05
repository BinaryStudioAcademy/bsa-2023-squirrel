namespace Squirrel.Shared.DTO.Text;

public sealed class SideBySideDiffResultDto
{
    public List<DiffLineResult> OldTextLines { get; set; } = new();
    public List<DiffLineResult> NewTextLines { get; set; } = new();
    public bool HasDifferences { get; set; }
}
