using System.Net;
using Squirrel.Shared.Enums;
using Squirrel.Shared.Exceptions.Abstract;

namespace Squirrel.Shared.Exceptions;

public class QueryExpiredException : RequestException
{
    public QueryExpiredException(Guid queryId) : base(
        $"Query with ID: {queryId} is already exist in project",
        ErrorType.InvalidQuery,
        HttpStatusCode.BadRequest)
    {
    }
}