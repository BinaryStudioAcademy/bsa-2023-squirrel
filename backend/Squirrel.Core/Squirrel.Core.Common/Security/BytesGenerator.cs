using System.Security.Cryptography;

namespace Squirrel.Core.Common.Security;

public static class BytesGenerator
{
    public const int DefaultLength = 32;
    
    public static byte[] GetRandomBytes(int length = DefaultLength)
    {
        using var randomNumberGenerator = new RNGCryptoServiceProvider();
        var salt = new byte[length];
        randomNumberGenerator.GetBytes(salt);

        return salt;
    }
}