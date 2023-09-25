namespace Squirrel.Shared.DTO.Table;

public class TableDataDto: BaseDbItem
{
    public int TotalRows { get; set; }
    public List<Dictionary<string, string>> Rows { get; set; } = new();
}