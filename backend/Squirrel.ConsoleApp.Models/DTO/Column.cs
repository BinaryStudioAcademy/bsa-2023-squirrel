namespace Squirrel.ConsoleApp.Models.DTO
{
    public class Column
    {
        public string ColumnName { get; set; } = null!;
        public int? ColumnOrder { get; set; }
        public string DataType { get; set; } = null!;
        public string Default { get; set; } = null!;
        public int? Precision { get; set; }
        public int? Scale { get; set; }
        public bool? AllowNulls { get; set; }
        public bool? Identity { get; set; }
        public bool? PrimaryKey { get; set; }
        public bool? ForeignKey { get; set; }
        public string RelatedTable { get; set; } = null!;
        public string RelatedTableColumn { get; set; } = null!;
        public string RelatedTableSchema { get; set; } = null!;
        public string Description { get; set; } = null!;
    }
}
