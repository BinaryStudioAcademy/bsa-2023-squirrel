namespace Squirrel.Shared.DTO.Text;

public sealed class InLineDiffResultDto
{
    public List<DiffLineResult> DiffLinesResults { get; set; } = new();
    public bool HasDifferences { get; set; }
}
