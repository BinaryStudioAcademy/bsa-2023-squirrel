﻿namespace Squirrel.ConsoleApp.Models.DTO
{
    public class TableStructureDto
    {
        public string Schema { get; set; } = null!;
        public string TableName { get; set; } = null!;
        public List<StructureRow> Rows { get; set; } = new();
    }
}