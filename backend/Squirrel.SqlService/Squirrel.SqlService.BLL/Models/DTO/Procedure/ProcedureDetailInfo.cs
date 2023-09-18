namespace Squirrel.SqlService.BLL.Models.DTO.Procedure;

public class ProcedureDetailInfo
{
    public string Schema { get; set; } = null!;
    public string Name { get; set; } = null!;
    public string Definition { get; set; } = null!;
}