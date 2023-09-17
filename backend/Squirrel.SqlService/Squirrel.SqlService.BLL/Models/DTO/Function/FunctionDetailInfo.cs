using Squirrel.SqlService.BLL.Models.DTO.Abstract;

namespace Squirrel.SqlService.BLL.Models.DTO.Function;

public class FunctionDetailInfo: BaseDbItem
{
    public string ReturnedType { get; set; } = null!;
    public bool? IsUserDefined { get; set; }
    public string Definition { get; set; } = null!;
}