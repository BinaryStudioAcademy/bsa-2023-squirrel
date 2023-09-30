namespace Squirrel.Shared.DTO.Function;

public class FunctionDetailInfo : BaseDbItemWithDefinition
{
    public string ReturnedType { get; set; } = null!;
    public bool? IsUserDefined { get; set; }
}