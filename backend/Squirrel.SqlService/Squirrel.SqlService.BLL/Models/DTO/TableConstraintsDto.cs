﻿using Squirrel.SqlService.BLL.Models.DTO.Abstract;

namespace Squirrel.SqlService.BLL.Models.DTO;

public class TableConstraintsDto
{
    public List<Constraint> Constraints { get; set; } = new();
}