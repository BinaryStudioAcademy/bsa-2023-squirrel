﻿namespace Squirrel.SqlService.BLL.Models.DTO;

public class Constraint
{
    public string ConstraintName { get; set; } = null!;
    public string Columns { get; set; } = null!;
    public string CheckClause { get; set; } = null!;
}