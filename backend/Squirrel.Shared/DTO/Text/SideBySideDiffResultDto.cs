namespace Squirrel.Shared.DTO.Text;

public class SideBySideDiffResultDto
{
    public List<DiffLineResult> OldTextLines { get; set; } = new();
    public List<DiffLineResult> NewTextLines { get; set; } = new();
    public bool HasDifferences { get; set; }
}
