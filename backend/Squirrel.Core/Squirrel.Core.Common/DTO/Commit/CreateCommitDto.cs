using Squirrel.Shared.DTO.SelectedItems;

namespace Squirrel.Core.Common.DTO.Commit;
public class CreateCommitDto
{
    public string Message { get; set; } = null!;
    public ICollection<TreeNodeDto> SelectedItems { get; set; } = new List<TreeNodeDto>();
    public int BranchId { get; set; }
    public string ChangesGuid { get; set; } = null!;
    public string PreScript { get; set; } = null!;
    public string PostScript { get; set; } = null!;
}