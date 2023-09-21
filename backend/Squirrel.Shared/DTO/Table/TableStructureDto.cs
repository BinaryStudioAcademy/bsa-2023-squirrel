using Squirrel.SqlService.BLL.Models.DTO;

namespace Squirrel.Shared.DTO.Table;

public class TableStructureDto: BaseDbItem
{
    public List<TableColumnInfo> Columns { get; set; } = new();
}