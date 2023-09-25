namespace Squirrel.SqlService.BLL.Models.DTO.UserDefinedType.TableType;

public class UserDefinedTableDetailsDto: BaseDbItem
{
    public List<UserDefinedTableTypeColumnInfo> Columns { get; set; } = new();
}