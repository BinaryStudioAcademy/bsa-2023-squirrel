using System.Security.Claims;
using Squirrel.Core.BLL.Interfaces;

namespace Squirrel.Core.BLL.Services;

public sealed class CurrentUserIdService : ICurrentUserIdService
{
    public int? CurrentUserId { get; private set; }
    
    public void SetCurrentUserIdFromClaims(IEnumerable<Claim> claims)
    {
        var id = claims.FirstOrDefault(x => x.Type == "id")?.Value;
        CurrentUserId = id is null ? null : int.Parse(id);
    }
}