using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Squirrel.Core.Common.DTO.Branch;
public class BranchCreateDto
{
    public string Name { get; set; } = null!;
    public int? ParentId { get; set; } = null!;
}
