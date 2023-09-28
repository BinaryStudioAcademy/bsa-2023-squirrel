using Microsoft.AspNetCore.SignalR;
using MongoDB.Bson;
using Newtonsoft.Json;
using Squirrel.ConsoleApp.Models;
using Squirrel.Core.Common.DTO.Script;
using Squirrel.Shared.DTO;
using Squirrel.Shared.DTO.DatabaseItem;
using Squirrel.Shared.DTO.Function;
using Squirrel.Shared.DTO.Procedure;
using Squirrel.Shared.DTO.View;
using Squirrel.Shared.Enums;
using Squirrel.SqlService.BLL.Interfaces;
using Squirrel.SqlService.BLL.Interfaces.ConsoleAppHub;
using System.Text;
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
            if (contentCompare.ItemType == DatabaseItemType.Function)
            {
                await ExecuteApplyChangesRoutinesViewsAsync<FunctionDetailInfo>(contentCompare, applyChangesDto, DatabaseItemType.Function);
            }
            if (contentCompare.ItemType == DatabaseItemType.StoredProcedure)
            {
                await ExecuteApplyChangesRoutinesViewsAsync<ProcedureDetailInfo>(contentCompare, applyChangesDto, DatabaseItemType.Procedure);
            }
            if (contentCompare.ItemType == DatabaseItemType.View)
            {
                await ExecuteApplyChangesRoutinesViewsAsync<ViewDetailInfo>(contentCompare, applyChangesDto, DatabaseItemType.View);
            }
        }
    }

    private async Task ExecuteApplyChangesRoutinesViewsAsync<T>(DatabaseItemContentCompare contentCompare, ApplyChangesDto applyChangesDto, DatabaseItemType dbItemType) where T : BaseDbItemWithDefinition
    {
        var newChanges = contentCompare.SideBySideDiff!.NewTextLines.FirstOrDefault() ?? throw new ArgumentNullException(nameof(contentCompare.SideBySideDiff.NewTextLines));
        var oldChanges = contentCompare.SideBySideDiff.OldTextLines.FirstOrDefault() ?? throw new ArgumentNullException(nameof(contentCompare.SideBySideDiff.OldTextLines));
        if (newChanges.Type == DiffPlex.DiffBuilder.Model.ChangeType.Modified)
        {
            if (newChanges.Text == "null")
            {
                var dropScript = $"DROP {dbItemType} IF EXISTS [{contentCompare.SchemaName}].[{contentCompare.ItemName}]";
                await ExecuteScript(applyChangesDto, dropScript);
            }
            else
            {
                var itemDetailsDto = JsonConvert.DeserializeObject<T>(newChanges.Text)!;
                var alterScript = itemDetailsDto.Definition.Replace("CREATE", "CREATE OR ALTER", true, null);
                await ExecuteScript(applyChangesDto, alterScript);
            }
        }
        if (newChanges.Type == DiffPlex.DiffBuilder.Model.ChangeType.Inserted)
        {
            if (oldChanges.Text is null)
            {
                var itemDetailsDto = JsonConvert.DeserializeObject<T>(newChanges.Text)!;
                await ExecuteScript(applyChangesDto, itemDetailsDto.Definition);
            }
        }
    }

    //private async Task ExecuteApplyFunctionChangesAsync(DatabaseItemContentCompare contentCompare, ApplyChangesDto applyChangesDto)
    //{
    //    var newChanges = contentCompare.SideBySideDiff!.NewTextLines.FirstOrDefault() ?? throw new ArgumentNullException(nameof(contentCompare.SideBySideDiff.NewTextLines));
    //    var oldChanges = contentCompare.SideBySideDiff.OldTextLines.FirstOrDefault() ?? throw new ArgumentNullException(nameof(contentCompare.SideBySideDiff.OldTextLines));
    //    if (newChanges.Type == DiffPlex.DiffBuilder.Model.ChangeType.Modified)
    //    {
    //        if (newChanges.Text == "null")
    //        {
    //            var dropScript = $"DROP FUNCTION IF EXISTS [{contentCompare.SchemaName}].[{contentCompare.ItemName}]";
    //            await ExecuteScript(applyChangesDto, dropScript);
    //        }
    //        else
    //        {
    //            var itemDetailsDto = JsonConvert.DeserializeObject<FunctionDetailInfo>(newChanges.Text)!;
    //            var alterScript = itemDetailsDto.Definition.Replace("CREATE", "CREATE OR ALTER", true, null);
    //            await ExecuteScript(applyChangesDto, alterScript);
    //        }
    //    }
    //    if (newChanges.Type == DiffPlex.DiffBuilder.Model.ChangeType.Inserted)
    //    {
    //        if (oldChanges.Text is null)
    //        {
    //            var itemDetailsDto = JsonConvert.DeserializeObject<FunctionDetailInfo>(newChanges.Text)!;
    //            await ExecuteScript(applyChangesDto, itemDetailsDto.Definition);
    //        }
    //    }
    //}
    //private async Task ExecuteApplySpChangesAsync(DatabaseItemContentCompare contentCompare, ApplyChangesDto applyChangesDto)
    //{
    //    var newChanges = contentCompare.SideBySideDiff!.NewTextLines.FirstOrDefault() ?? throw new ArgumentNullException(nameof(contentCompare.SideBySideDiff.NewTextLines));
    //    var oldChanges = contentCompare.SideBySideDiff.OldTextLines.FirstOrDefault() ?? throw new ArgumentNullException(nameof(contentCompare.SideBySideDiff.OldTextLines));
    //    if (newChanges.Type == DiffPlex.DiffBuilder.Model.ChangeType.Modified)
    //    {
    //        if (newChanges.Text == "null")
    //        {
    //            var dropScript = $"DROP PROCEDURE IF EXISTS [{contentCompare.SchemaName}].[{contentCompare.ItemName}]";
    //            await ExecuteScript(applyChangesDto, dropScript);
    //        }
    //        else
    //        {
    //            var itemDetailsDto = JsonConvert.DeserializeObject<ProcedureDetailInfo>(newChanges.Text)!;
    //            var alterScript = itemDetailsDto.Definition.Replace("CREATE", "CREATE OR ALTER", true, null);
    //            await ExecuteScript(applyChangesDto, alterScript);
    //        }
    //    }
    //    if (newChanges.Type == DiffPlex.DiffBuilder.Model.ChangeType.Inserted)
    //    {
    //        if (oldChanges.Text is null)
    //        {
    //            var itemDetailsDto = JsonConvert.DeserializeObject<ProcedureDetailInfo>(newChanges.Text)!;
    //            await ExecuteScript(applyChangesDto, itemDetailsDto.Definition);
    //        }
    //    }
    //}

    //private async Task ExecuteApplyViewChangesAsync(DatabaseItemContentCompare contentCompare, ApplyChangesDto applyChangesDto)
    //{
    //    var newChanges = contentCompare.SideBySideDiff!.NewTextLines.FirstOrDefault() ?? throw new ArgumentNullException(nameof(contentCompare.SideBySideDiff.NewTextLines));
    //    var oldChanges = contentCompare.SideBySideDiff.OldTextLines.FirstOrDefault() ?? throw new ArgumentNullException(nameof(contentCompare.SideBySideDiff.OldTextLines));
    //    if (newChanges.Type == DiffPlex.DiffBuilder.Model.ChangeType.Modified)
    //    {
    //        if (newChanges.Text == "null")
    //        {
    //            var dropScript = $"DROP VIEW IF EXISTS [{contentCompare.SchemaName}].[{contentCompare.ItemName}]";
    //            await ExecuteScript(applyChangesDto, dropScript);
    //        }
    //        else
    //        {
    //            var itemDetailsDto = JsonConvert.DeserializeObject<ViewDetailInfo>(newChanges.Text)!;
    //            var alterScript = itemDetailsDto.Definition.Replace("CREATE", "CREATE OR ALTER", true, null);
    //            await ExecuteScript(applyChangesDto, alterScript);
    //        }
    //    }
    //    if (newChanges.Type == DiffPlex.DiffBuilder.Model.ChangeType.Inserted)
    //    {
    //        if (oldChanges.Text is null)
    //        {
    //            var itemDetailsDto = JsonConvert.DeserializeObject<ViewDetailInfo>(newChanges.Text)!;
    //            await ExecuteScript(applyChangesDto, itemDetailsDto.Definition);
    //        }
    //    }
    //}

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
