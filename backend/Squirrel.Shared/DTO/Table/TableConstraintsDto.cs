namespace Squirrel.Shared.DTO.Table;

public class TableConstraintsDto : BaseDbItem
{
    public List<Constraint> Constraints { get; set; } = new();
}