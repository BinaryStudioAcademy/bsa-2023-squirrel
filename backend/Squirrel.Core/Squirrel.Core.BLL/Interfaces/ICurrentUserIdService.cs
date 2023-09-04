using System.Security.Claims;

namespace Squirrel.Core.BLL.Interfaces;

public interface ICurrentUserIdService
{
    int? CurrentUserId { get; }
    void SetCurrentUserIdFromClaims(IEnumerable<Claim> claims);
}