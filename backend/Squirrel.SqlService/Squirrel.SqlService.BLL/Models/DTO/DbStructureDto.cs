using Squirrel.SqlService.BLL.Models.DTO.Function;
using Squirrel.SqlService.BLL.Models.DTO.Procedure;

namespace Squirrel.SqlService.BLL.Models.DTO;

public class DbStructureDto
{
    public List<TableStructureDto>? TableStructures { get; set; }
    public List<TableConstraintsDto>? Constraints { get; set; }
    public FunctionDetailsDto? FunctionDetails { get; set; }
    public ProcedureDetailsDto? ProcedureDetails { get; set; }
}
