namespace Squirrel.SqlService.BLL.Models.DTO.UserDefinedType.DataType;

public class UserDefinedDataTypeDetailInfo: BaseDbItem
{
    public string BaseType { get; set; } = null!;
    public int? MaxLength { get; set; }
    public int? Precision { get; set; }
    public int? Scale { get; set; }
    public bool? AllowNulls { get; set; }
    public bool? IsTable { get; set; } 
    public string? Definition { get; set; } 
    public string Default { get; set; } = null!;
    public string ConstraintName { get; set; } = null!;
    public string ConstraintDefinition { get; set; } = null!;

}