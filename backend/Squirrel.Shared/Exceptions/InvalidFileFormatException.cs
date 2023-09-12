using System.Net;
using Squirrel.Shared.Enums;
using Squirrel.Shared.Exceptions.Abstract;

namespace Squirrel.Shared.Exceptions;

public class InvalidFileFormatException : RequestException
{
    public InvalidFileFormatException(string types) : base(
        $"Invalid file type, need {types}", 
        ErrorType.InvalidFileFormat,
        HttpStatusCode.UnsupportedMediaType)
    {
    }
}