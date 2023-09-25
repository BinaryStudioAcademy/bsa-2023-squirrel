using System.Net;
using Squirrel.Shared.Enums;
using Squirrel.Shared.Exceptions.Abstract;

namespace Squirrel.Shared.Exceptions;

public class QueryAlreadyExistException : RequestException
{
    public QueryAlreadyExistException(Guid queryId) : base(
        $"Query with ID: {queryId} was not registered or has expired",
        ErrorType.QueryExpired,
        HttpStatusCode.BadRequest)
    {
    }
}