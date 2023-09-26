namespace Squirrel.Shared.DTO.Text;

public sealed class SideBySideDiffResultDto
{
    public ICollection<DiffLineResult> OldTextLines { get; set; } = new List<DiffLineResult>();
    public ICollection<DiffLineResult> NewTextLines { get; set; } = new List<DiffLineResult>();
    public bool HasDifferences { get; set; }
}
