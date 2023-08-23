using Squirrel.Core.DAL.Entities.Common;

namespace Squirrel.Core.DAL.Entities;

public sealed class User : Entity<long>
{
    public string Username { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Email { get; set; }
    public string Password { get; set; }
}