using Squirrel.SqlService.BLL.Models.DTO.Abstract;

namespace Squirrel.SqlService.BLL.Models.DTO.Procedure;

public class ProcedureDetailInfo: BaseDbItem
{
    public string Definition { get; set; } = null!;
}