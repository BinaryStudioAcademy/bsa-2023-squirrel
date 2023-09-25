namespace Squirrel.Shared.DTO.Function;

public class FunctionDetailInfo : BaseDbItem
{
    public string ReturnedType { get; set; } = null!;
    public bool? IsUserDefined { get; set; }
    public string Definition { get; set; } = null!;
}