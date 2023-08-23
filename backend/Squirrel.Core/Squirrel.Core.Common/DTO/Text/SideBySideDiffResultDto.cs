namespace Squirrel.Core.Common.DTO.Text;

public class SideBySideDiffResultDto
{
    public List<DiffLineResult> OldTextLines { get; set; }
    public List<DiffLineResult> NewTextLines { get; set; }
    public bool HasDifferences { get; set; }
}
