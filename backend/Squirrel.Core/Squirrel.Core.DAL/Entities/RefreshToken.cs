using Squirrel.Core.DAL.Entities.Common.AuditEntity;

namespace Squirrel.Core.DAL.Entities;

public sealed class RefreshToken : AuditEntity<int>
{
    public string Token { get; set; } = string.Empty;
    public DateTime ExpiresAt { get; }
    
    public int UserId { get; set; }
    public User User { get; set; } = null!;

    private readonly TimeSpan _lifetime = TimeSpan.FromDays(1);

    public RefreshToken()
    {
        ExpiresAt = DateTime.UtcNow.Add(_lifetime);
    }

    public bool IsActive() => DateTime.UtcNow <= ExpiresAt;
}