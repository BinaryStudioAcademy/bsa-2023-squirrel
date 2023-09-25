using Squirrel.Shared.DTO.Function;
using Squirrel.Shared.DTO.Procedure;
using Squirrel.Shared.DTO.Table;
using Squirrel.SqlService.BLL.Models.DTO.View;

namespace Squirrel.Shared.DTO;

public class DbStructureDto
{
    public List<TableStructureDto> DbTableStructures { get; set; } = new();
    public List<TableConstraintsDto> DbConstraints { get; set; } = new();
    public FunctionDetailsDto DbFunctionDetails { get; set; } = new();
    public ProcedureDetailsDto DbProcedureDetails { get; set; } = new();
    public ViewDetailsDto DbViewsDetails { get; set; } = new();
}
