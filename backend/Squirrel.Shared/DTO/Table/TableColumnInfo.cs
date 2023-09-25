namespace Squirrel.Shared.DTO.Table;

public class TableColumnInfo
{
    public string ColumnName { get; set; } = null!;
    public int? ColumnOrder { get; set; }
    public string DataType { get; set; } = null!;
    public bool? IsUserDefined { get; set; }
    public string Default { get; set; } = null!;
    public int? Precision { get; set; }
    public int? Scale { get; set; }
    public int? MaxLength { get; set; }
    public bool? IsNullsAllowed { get; set; }
    public bool? IsIdentity { get; set; }
    public bool? IsPrimaryKey { get; set; }
    public bool? IsForeignKey { get; set; }
    public string RelatedTable { get; set; } = null!;
    public string RelatedTableColumn { get; set; } = null!;
    public string RelatedTableSchema { get; set; } = null!;
    public string Description { get; set; } = null!;
}