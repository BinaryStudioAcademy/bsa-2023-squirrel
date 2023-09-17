namespace Squirrel.SqlService.BLL.Models.DTO.Abstract;

public abstract class BaseDbItem
{
    public string Schema { get; set; } = null!;
    public string Name { get; set; } = null!;
}