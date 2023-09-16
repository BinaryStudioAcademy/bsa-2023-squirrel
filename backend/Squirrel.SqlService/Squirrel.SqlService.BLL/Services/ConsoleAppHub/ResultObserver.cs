using Squirrel.SqlService.BLL.Models.ConsoleAppHub;

namespace Squirrel.SqlService.BLL.Services.ConsoleAppHub;

public class ResultObserver
{
    private readonly Dictionary<Guid, TaskCompletionSource<QueryResultTableDTO>> _pendingRequests = new();

    public TaskCompletionSource<QueryResultTableDTO> Register(Guid httpId)
    {
        var tcs = new TaskCompletionSource<QueryResultTableDTO>();
        _pendingRequests[httpId] = tcs;
        return tcs;
    }

    public void SetResult(Guid httpId, QueryResultTableDTO queryResultTableDto)
    {
        if (_pendingRequests.TryGetValue(httpId, out var tcs))
        {
            tcs.SetResult(queryResultTableDto);
            _pendingRequests.Remove(httpId);
        }
    }
}