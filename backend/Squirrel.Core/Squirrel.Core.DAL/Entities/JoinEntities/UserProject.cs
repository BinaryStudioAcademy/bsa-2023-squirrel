using System.ComponentModel.DataAnnotations;
using Squirrel.Core.DAL.Enums;

namespace Squirrel.Core.DAL.Entities.JoinEntities;

public sealed class UserProject
{
    [Required]
    public UserRole UserRole { get; set; }

    public int ProjectId { get; set; }
    public int UserId { get; set; }
}