using Squirrel.SqlService.BLL.Models.DTO.Function;
using Squirrel.SqlService.BLL.Models.DTO.Procedure;
using Squirrel.SqlService.BLL.Models.DTO.View;

namespace Squirrel.SqlService.BLL.Models.DTO;

public class DbStructureDto
{
    public List<TableStructureDto>? DbTableStructures { get; set; } = new();
    public TableConstraintsDto? DbConstraints { get; set; } = new();
    public FunctionDetailsDto? DbFunctionDetails { get; set; } = new();
    public ProcedureDetailsDto? DbProcedureDetails { get; set; } = new();
    public ViewDetailsDto? DbViewDetails { get; set; } = new();
}