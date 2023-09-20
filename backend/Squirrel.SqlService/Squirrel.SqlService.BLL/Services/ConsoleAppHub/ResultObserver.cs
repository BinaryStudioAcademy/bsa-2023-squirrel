using Squirrel.ConsoleApp.Models;
using Squirrel.Shared.Exceptions;
using System.Collections.Concurrent;

namespace Squirrel.SqlService.BLL.Services.ConsoleAppHub;

public class ResultObserver
{
    private readonly ConcurrentDictionary<Guid, TaskCompletionSource<QueryResultTable>> _pendingRequests = new();
    private const int SecondsToTimeout = 30;

    public TaskCompletionSource<QueryResultTable> Register(Guid queryId)
    {
        var tcs = new TaskCompletionSource<QueryResultTable>();
        if (!_pendingRequests.TryAdd(queryId, tcs))
        {
            throw new QueryAlreadyExistException(queryId);
        }

        _ = RemoveIfNotSet(queryId);

        return tcs;
    }

    public void SetResult(Guid queryId, QueryResultTable queryResultTableDto)
    {
        if (_pendingRequests.TryRemove(queryId, out var tcs))
        {
            tcs.TrySetResult(queryResultTableDto);
        }
        else
        {
            throw new QueryExpiredException(queryId);
        }
    }

    private async Task RemoveIfNotSet(Guid queryId)
    {
        await Task.Delay(TimeSpan.FromSeconds(SecondsToTimeout));
        if (_pendingRequests.TryRemove(queryId, out var removedTcs))
        {
            removedTcs.TrySetCanceled();
        }
        else
        {
            throw new QueryExpiredException(queryId);
        }
    }
}