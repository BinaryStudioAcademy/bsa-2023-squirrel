namespace Squirrel.SqlService.BLL.Models.DTO;

public class TableDataDto: BaseDbItem
{
    public int TotalRows { get; set; }
    public List<Dictionary<string, string>> Rows { get; set; } = new();
}