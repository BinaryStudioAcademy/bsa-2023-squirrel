namespace Squirrel.Shared.DTO.UserDefinedType.TableType;

public class UserDefinedTableDetailsDto: BaseDbItem
{
    public List<UserDefinedTableTypeColumnInfo> Columns { get; set; } = new();
}