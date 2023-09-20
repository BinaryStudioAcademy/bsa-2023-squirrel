using Squirrel.Core.Common.DTO.Branch;
using Squirrel.Core.Common.DTO.Users;

namespace Squirrel.Core.Common.DTO.Commit;
public class CommitDto
{
    public int Id { get; set; }
    public string Message { get; set; } = string.Empty;
    public UserDto Author { get; set; } = null!;
    public ICollection<BranchDto> Branches = new List<BranchDto>();
    public string PreScript { get; set; } = null!;
    public string PostScript { get; set; } = null!;
}
