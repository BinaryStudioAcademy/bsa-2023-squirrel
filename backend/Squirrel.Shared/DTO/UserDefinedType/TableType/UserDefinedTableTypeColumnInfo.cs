namespace Squirrel.Shared.DTO.UserDefinedType.TableType;

public class UserDefinedTableTypeColumnInfo
{
    public string ColumnName { get; set; } = null!;
    public int? ColumnOrder { get; set; }
    public string DataType { get; set; } = null!;
    public bool? IsUserDefined { get; set; }
    public int? Precision { get; set; }
    public int? Scale { get; set; }
    public int? MaxLength { get; set; }
    public bool? AllowNulls { get; set; }
    public string? Default { get; set; } 
    public bool? IsIdentity { get; set; } 
    public bool? IsPrimaryKey { get; set; } 
    
}