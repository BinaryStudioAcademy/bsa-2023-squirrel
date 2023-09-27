using Microsoft.AspNetCore.SignalR;
using Squirrel.ConsoleApp.Models;
using Squirrel.Core.Common.DTO.Script;
using Squirrel.Shared.DTO;
using Squirrel.Shared.DTO.DatabaseItem;
using Squirrel.Shared.Enums;
using Squirrel.SqlService.BLL.Interfaces;
using Squirrel.SqlService.BLL.Interfaces.ConsoleAppHub;
using ConsoleHub = Squirrel.SqlService.BLL.Hubs.ConsoleAppHub;


namespace Squirrel.SqlService.BLL.Services;

public class ApplyChangesService : IApplyChangesService
{
    private readonly IContentDifferenceService _contentDifferenceService;
    private readonly IDbItemsRetrievalService _dbItemsRetrievalService;
    private readonly ISqlFormatterService _sqlFormatterService;
    private readonly IResultObserver _resultObserver;
    private readonly (Guid queryId, TaskCompletionSource<QueryResultTable> tcs) _queryParameters;
    private readonly IHubContext<ConsoleHub, IExecuteOnClientSide> _hubContext;

    public ApplyChangesService(IContentDifferenceService contentDifferenceService, IDbItemsRetrievalService dbItemsRetrievalService, 
        IHubContext<ConsoleHub, IExecuteOnClientSide> hubContext, IResultObserver resultObserver, ISqlFormatterService sqlFormatterService)
    {
        _contentDifferenceService = contentDifferenceService;
        _dbItemsRetrievalService = dbItemsRetrievalService;
        _resultObserver = resultObserver;
        _hubContext = hubContext;
        _queryParameters = RegisterQuery();
        _sqlFormatterService = sqlFormatterService;
    }

    public async Task ApplyChanges(ApplyChangesDto applyChangesDto, int commitId)
    {
        var currentDbStructure = await _dbItemsRetrievalService.GetAllDbStructureAsync(Guid.Parse(applyChangesDto.ClientId!));
        var contentDifferenceList = await _contentDifferenceService.GetContentDiffsAsync(commitId, currentDbStructure, true);
        if (!contentDifferenceList.Any())
        {
            //TODO
            throw new Exception("List of Database changes is empty");
        }
        foreach (var contentCompare in contentDifferenceList)
        {
            if (contentCompare.SideBySideDiff is null)
            {
                //TODO
                throw new ArgumentNullException(nameof(contentCompare.SideBySideDiff));
            }
            if (!contentCompare.SideBySideDiff.HasDifferences)
            {
                continue;
            }
            if (contentCompare.ItemType == DatabaseItemType.Function || contentCompare.ItemType == DatabaseItemType.StoredProcedure)
            {
                var changes = contentCompare.SideBySideDiff.NewTextLines.FirstOrDefault();
                if (changes.Type == DiffPlex.DiffBuilder.Model.ChangeType.Modified)
                {
                    var alterScript = GetRoutineAlterScript(changes.Text);
                    await ExecuteScript(applyChangesDto, alterScript);
                }
            }
        }
    }

    private string GetRoutineAlterScript(string modifiedScript)
    {
        return modifiedScript.Replace("CREATE", "CREATE OR ALTER", true, null);
    }

    private async Task ExecuteScript(ApplyChangesDto applyChangesDto, string script)
    {
        var formattedScript = _sqlFormatterService.GetFormattedSql(applyChangesDto.DbEngine, script);
        await _hubContext.Clients.User(applyChangesDto.ClientId!)
                         .ExecuteScriptAsync(_queryParameters.queryId, formattedScript.Content!);
    }
    private (Guid queryId, TaskCompletionSource<QueryResultTable> tcs) RegisterQuery()
    {
        var queryId = Guid.NewGuid();
        var tcs = _resultObserver.Register(queryId);
        return (queryId, tcs);
    }
}
