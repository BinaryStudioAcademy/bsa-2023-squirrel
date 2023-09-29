using Squirrel.Shared.DTO.Function;
using Squirrel.Shared.DTO.Procedure;
using Squirrel.Shared.DTO.Table;
using Squirrel.Shared.DTO.UserDefinedType.DataType;
using Squirrel.Shared.DTO.UserDefinedType.TableType;
using Squirrel.Shared.DTO.View;

namespace Squirrel.Shared.DTO;

public class DbStructureDto
{
    public List<TableStructureDto> DbTableStructures { get; set; } = new();
    public List<TableConstraintsDto> DbConstraints { get; set; } = new();

    public UserDefinedDataTypeDetailsDto DbUserDefinedDataTypeDetailsDto { get; set; } = new();
    public UserDefinedTables DbUserDefinedTableTypeDetailsDto { get; set; } = new();
    public FunctionDetailsDto DbFunctionDetails { get; set; } = new();
    public ProcedureDetailsDto DbProcedureDetails { get; set; } = new();
    public ViewDetailsDto DbViewsDetails { get; set; } = new();
}
