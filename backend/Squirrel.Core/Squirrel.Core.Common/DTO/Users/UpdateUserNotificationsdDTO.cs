namespace Squirrel.Core.Common.DTO.Users;

public class UpdateUserNotificationsdDTO
{
    public int Id { get; set; }
    public bool SquirrelNotification { get; set; }
    public bool EmailNotification { get; set; }
}
