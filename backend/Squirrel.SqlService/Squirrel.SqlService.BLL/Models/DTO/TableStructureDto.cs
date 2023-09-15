namespace Squirrel.SqlService.BLL.Models.DTO;

public class TableStructureDto
{
    public string Schema { get; set; } = null!;
    public string TableName { get; set; } = null!;
    public List<TableColumnInfo> Columns { get; set; } = new();
}