namespace Squirrel.SqlService.BLL.Models.DTO;

public class TableStructureDto: BaseDbItem
{
    public List<TableColumnInfo> Columns { get; set; } = new();
}