using Squirrel.SqlService.BLL.Models.DTO;

namespace Squirrel.Shared.DTO.Table;

public class TableConstraintsDto
{
    public List<Constraint> Constraints { get; set; } = new();
}