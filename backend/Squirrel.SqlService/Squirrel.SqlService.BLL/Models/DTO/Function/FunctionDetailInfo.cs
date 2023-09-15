namespace Squirrel.SqlService.BLL.Models.DTO.Function;

public class FunctionDetailInfo
{
    public string Schema { get; set; } = null!;
    public string Name { get; set; } = null!;
    public string ReturnedType { get; set; } = null!;
    public bool? IsUserDefined { get; set; }
    public string Definition { get; set; } = null!;
}