using System.Collections.Concurrent;
using Squirrel.Shared.Exceptions;
using Squirrel.SqlService.BLL.Models.ConsoleAppHub;

namespace Squirrel.SqlService.BLL.Services.ConsoleAppHub;

public class ResultObserver
{
    private readonly ConcurrentDictionary<Guid, TaskCompletionSource<QueryResultTableDTO>> _pendingRequests = new();
    private const int SecondsToTimeout = 20;

    public TaskCompletionSource<QueryResultTableDTO> Register(Guid queryId)
    {
        var tcs = new TaskCompletionSource<QueryResultTableDTO>();
        if (!_pendingRequests.TryAdd(queryId, tcs))
        {
            throw new QueryAlreadyExistException(queryId);
        }

        _ = RemoveIfNotSet(queryId);

        return tcs;
    }

    public void SetResult(Guid queryId, QueryResultTableDTO queryResultTableDto)
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