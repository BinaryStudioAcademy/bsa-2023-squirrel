﻿using Squirrel.SqlService.BLL.Models.DTO.Function;
using Squirrel.SqlService.BLL.Models.DTO.Procedure;

namespace Squirrel.SqlService.BLL.Models.DTO;

public class DbStructureDto
{
    public List<TableStructureDto>? DbTableStructures { get; set; } = new();
    public TableConstraintsDto? DbConstraints { get; set; } = new();
    public FunctionDetailsDto? DbFunctionDetails { get; set; } = new();
    public ProcedureDetailsDto? DbProcedureDetails { get; set; } = new();
}
