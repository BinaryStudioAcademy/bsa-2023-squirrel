namespace Squirrel.SqlService.BLL.Models.DTO;

public class TableDataDto
{
    public string Schema { get; set; } = null!;
    public string Name { get; set; } = null!;
    public int TotalRows { get; set; }
    public List<Dictionary<string, string>> Rows { get; set; } = new();
}