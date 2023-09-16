using Squirrel.SqlService.BLL.Models.ConsoleAppHub;

namespace Squirrel.SqlService.BLL.Services.ConsoleAppHub;

public class ResultObserver
{
    private readonly Dictionary<Guid, TaskCompletionSource<QueryResultTableDTO>> _pendingRequests = new();

    public TaskCompletionSource<QueryResultTableDTO> Register(Guid queryId)
    {
        var tcs = new TaskCompletionSource<QueryResultTableDTO>();
        _pendingRequests[queryId] = new TaskCompletionSource<QueryResultTableDTO>();
        return tcs;
    }

    public void SetResult(Guid queryId, QueryResultTableDTO queryResultTableDto)
    {
        if (_pendingRequests.TryGetValue(queryId, out var tcs))
        {
            tcs.SetResult(queryResultTableDto);
            _pendingRequests.Remove(queryId);
        }
    }
}