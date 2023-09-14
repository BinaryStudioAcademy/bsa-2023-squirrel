using Squirrel.Shared.Enums;
using Squirrel.Shared.Exceptions.Abstract;
using System.Net;

namespace Squirrel.Shared.Exceptions;

public sealed class SqlSyntaxException : RequestException
{
    public SqlSyntaxException(string parserErrorMessage) : base(
        parserErrorMessage,
        ErrorType.SqlSyntax,
        HttpStatusCode.BadRequest)
    {
    }
}