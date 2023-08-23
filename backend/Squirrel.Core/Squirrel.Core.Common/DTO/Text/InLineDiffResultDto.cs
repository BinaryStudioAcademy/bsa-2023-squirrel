namespace Squirrel.Core.Common.DTO.Text;

public class InLineDiffResultDto
{
    public List<DiffLineResult> DiffLinesResults { get; set; } = new List<DiffLineResult>();
    public bool HasDifferences { get; set; }
}
