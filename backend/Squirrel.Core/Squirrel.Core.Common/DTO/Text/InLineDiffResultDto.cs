namespace Squirrel.Core.Common.DTO.Text;

public class InLineDiffResultDto
{
    public List<DiffLineResult> DiffLinesResults { get; set; }
    public bool HasDifferences { get; set; }
}
