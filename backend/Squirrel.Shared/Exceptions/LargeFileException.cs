using System.Net;
using Squirrel.Shared.Enums;
using Squirrel.Shared.Exceptions.Abstract;

namespace Squirrel.Shared.Exceptions;

public class LargeFileException : RequestException
{
    public LargeFileException(string maxSize) : base(
        $"The file size should not exceed {maxSize}",
        ErrorType.LargeFile,
        HttpStatusCode.BadRequest)
    {
    }
}