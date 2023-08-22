namespace Squirrel.Core.DAL.Entities;

public sealed class RefreshToken : AuditEntity<long>
{
    public string Token { get; set; } = string.Empty;
    public int UserId { get; set; }
    public DateTime ExpiresAt { get; }
    
    private readonly TimeSpan _lifetime = TimeSpan.FromDays(1);

    public RefreshToken()
    {
        ExpiresAt = DateTime.UtcNow.Add(_lifetime);
    }

    public bool IsActive() => DateTime.UtcNow <= ExpiresAt;
}