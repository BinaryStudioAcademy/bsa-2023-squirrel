namespace Squirrel.Core.Common.Security;

using Microsoft.AspNetCore.Cryptography.KeyDerivation;

public static class PasswordProcessor
{
    public static string HashPassword(string password, byte[] salt)
        => Convert.ToBase64String(
            KeyDerivation.Pbkdf2(
                password,
                salt,
                prf: KeyDerivationPrf.HMACSHA256,
                iterationCount: 10000,
                numBytesRequested: 32
            )
        );
    
    public static bool ValidatePassword(string password, string hash, string salt)
        => HashPassword(password, Convert.FromBase64String(salt)) == hash;
}