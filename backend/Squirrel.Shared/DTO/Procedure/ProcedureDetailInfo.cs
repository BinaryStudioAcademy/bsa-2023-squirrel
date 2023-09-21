using Squirrel.SqlService.BLL.Models.DTO;

namespace Squirrel.Shared.DTO.Procedure;

public class ProcedureDetailInfo: BaseDbItem
{
    public string Definition { get; set; } = null!;
}