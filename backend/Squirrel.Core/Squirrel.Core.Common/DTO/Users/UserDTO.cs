﻿namespace Squirrel.Core.Common.DTO.Users;

public class UserDTO
{
    public int Id { get; set; }
    public string UserName { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Email { get; set; }
    public string? AvatarUrl { get; set; }
    public bool SquirrelNotification { get; set; }
    public bool EmailNotification { get; set; }
}
