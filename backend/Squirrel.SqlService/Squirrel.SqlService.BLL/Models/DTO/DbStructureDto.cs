using Squirrel.SqlService.BLL.Models.DTO.Function;
using Squirrel.SqlService.BLL.Models.DTO.Procedure;

namespace Squirrel.SqlService.BLL.Models.DTO;

public class DbStructureDto
{
    public List<TableStructureDto>? TableStructures { get; set; } = new();
    public TableConstraintsDto? Constraints { get; set; } = new();
    public FunctionDetailsDto? FunctionDetails { get; set; } = new();
    public ProcedureDetailsDto? ProcedureDetails { get; set; } = new();
}
