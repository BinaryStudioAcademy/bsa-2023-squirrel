using Squirrel.Core.Common.DTO.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Squirrel.Core.Common.DTO.Branch;
public class BranchDetailsDto: BranchDto
{
    public UserDto? LastUpdatedBy { get; set; } = null!;
    public int Ahead { get; set; }
    public int Behind { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime? UpdatedAt { get; set; } = null!;
}
