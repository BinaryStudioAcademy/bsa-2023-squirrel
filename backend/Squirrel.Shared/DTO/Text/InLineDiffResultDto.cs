namespace Squirrel.Shared.DTO.Text;

public class InLineDiffResultDto
{
    public List<DiffLineResult> DiffLinesResults { get; set; } = new();
    public bool HasDifferences { get; set; }
}
