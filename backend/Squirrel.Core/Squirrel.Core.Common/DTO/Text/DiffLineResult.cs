namespace Squirrel.Core.Common.DTO.Text;

using DiffPlex.DiffBuilder.Model;

public class DiffLineResult
{
    public ChangeType Type { get; set; }

    public int? Position { get; set; }

    public string Text { get; set; }

    public List<DiffPiece> SubPieces { get; set; }
}
