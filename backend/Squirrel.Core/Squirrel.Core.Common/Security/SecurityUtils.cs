using System.Security.Cryptography;
using Microsoft.AspNetCore.Cryptography.KeyDerivation;

namespace Squirrel.Core.Common.Security;

public static class SecurityUtils
{
    public const int DefaultBytesLength = 32;

    public static string GenerateRandomSalt()
    {
        using var randomNumberGenerator = RandomNumberGenerator.Create();
        var salt = new byte[DefaultBytesLength];
        randomNumberGenerator.GetBytes(salt);

        return Convert.ToBase64String(salt);
    }
    
    public static string HashPassword(string password, string salt)
        => Convert.ToBase64String(
            KeyDerivation.Pbkdf2(
                password,
                Convert.FromBase64String(salt),
                prf: KeyDerivationPrf.HMACSHA256,
                iterationCount: 10000,
                numBytesRequested: 32
            )
        );

    public static bool ValidatePassword(string password, string hash, string salt)
        => HashPassword(password, salt) == hash;
}