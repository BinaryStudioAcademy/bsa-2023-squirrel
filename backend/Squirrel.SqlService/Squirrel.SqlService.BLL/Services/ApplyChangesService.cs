using Microsoft.AspNetCore.SignalR;
using Newtonsoft.Json;
using Squirrel.ConsoleApp.Models;
using Squirrel.Shared.DTO;
using Squirrel.Shared.DTO.DatabaseItem;
using Squirrel.Shared.DTO.Function;
using Squirrel.Shared.DTO.Procedure;
using Squirrel.Shared.DTO.Table;
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
    private (Guid queryId, TaskCompletionSource<QueryResultTable> tcs) _queryParameters;
    private readonly IHubContext<ConsoleHub, IExecuteOnClientSide> _hubContext;

    public ApplyChangesService(IContentDifferenceService contentDifferenceService, IDbItemsRetrievalService dbItemsRetrievalService, 
        IHubContext<ConsoleHub, IExecuteOnClientSide> hubContext, IResultObserver resultObserver, ISqlFormatterService sqlFormatterService)
    {
        _contentDifferenceService = contentDifferenceService;
        _dbItemsRetrievalService = dbItemsRetrievalService;
        _resultObserver = resultObserver;
        _hubContext = hubContext;
        _sqlFormatterService = sqlFormatterService;
    }

    public async Task ApplyChanges(ApplyChangesDto applyChangesDto, int commitId)
    {
        var currentDbStructure = await _dbItemsRetrievalService.GetAllDbStructureAsync(Guid.Parse(applyChangesDto.ClientId!));
        var contentDifferenceList = await _contentDifferenceService.GetContentDiffsAsync(commitId, currentDbStructure, true);
        if (!contentDifferenceList.Any())
        {
            throw new Exception("List of Database changes is empty");
        }
        foreach (var contentCompare in contentDifferenceList)
        {
            if (contentCompare.SideBySideDiff is null)
            {
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
            if (contentCompare.ItemType == DatabaseItemType.Table)
            {
                await ExecuteApplyChangesTablesAsync(contentCompare, applyChangesDto, DatabaseItemType.Table);
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
                await ExecuteScriptAsync(applyChangesDto, dropScript);
            }
            else
            {
                var itemDetailsDto = JsonConvert.DeserializeObject<T>(newChanges.Text)!;
                var alterScript = itemDetailsDto.Definition.Replace("CREATE", "CREATE OR ALTER", true, null);
                await ExecuteScriptAsync(applyChangesDto, alterScript);
            }
        }
        if (newChanges.Type == DiffPlex.DiffBuilder.Model.ChangeType.Inserted)
        {
            if (oldChanges.Text is null)
            {
                var itemDetailsDto = JsonConvert.DeserializeObject<T>(newChanges.Text)!;
                await ExecuteScriptAsync(applyChangesDto, itemDetailsDto.Definition);
            }
        }
    }

    private async Task ExecuteApplyChangesTablesAsync(DatabaseItemContentCompare contentCompare, ApplyChangesDto applyChangesDto, DatabaseItemType dbItemType)
    {
        var newChanges = contentCompare.SideBySideDiff!.NewTextLines.FirstOrDefault() ?? throw new ArgumentNullException(nameof(contentCompare.SideBySideDiff.NewTextLines));
        var oldChanges = contentCompare.SideBySideDiff.OldTextLines.FirstOrDefault() ?? throw new ArgumentNullException(nameof(contentCompare.SideBySideDiff.OldTextLines));
        if (newChanges.Type == DiffPlex.DiffBuilder.Model.ChangeType.Modified)
        {
            if (newChanges.Text == "null")
            {
                var refFkTables = await GetAllReferencedFkTablesScriptAsync(applyChangesDto, contentCompare.SchemaName, contentCompare.ItemName);
                await DropAllReferencedFkTablesScriptAsync(applyChangesDto, refFkTables);
                var dropScript = $"DROP {dbItemType} IF EXISTS [{contentCompare.SchemaName}].[{contentCompare.ItemName}]";
                await ExecuteScriptAsync(applyChangesDto, dropScript);
            }
            //else
            //{
            //    var tableStructureDto = JsonConvert.DeserializeObject<TableStructureDto>(newChanges.Text)!;
            //    var alterScript = GenerateCreateTableScript(contentCompare.SchemaName, contentCompare.ItemName, tableStructureDto);
            //    await ExecuteScriptAsync(applyChangesDto, alterScript);
            //}
        }
        if (newChanges.Type == DiffPlex.DiffBuilder.Model.ChangeType.Inserted)
        {
            if (oldChanges.Text is null)
            {
                var tableStructureDto = JsonConvert.DeserializeObject<TableStructureDto>(newChanges.Text)!;
                await ExecuteScriptAsync(applyChangesDto, GenerateCreateTableScript(contentCompare.SchemaName, contentCompare.ItemName, tableStructureDto));
            }
        }
    }

    private string GenerateCreateTableScript(string tableSchema, string tableName, TableStructureDto tableStructure)
    {
        StringBuilder createScript = new StringBuilder($"CREATE TABLE [{tableSchema}].[{tableName}] (");
        foreach (var column in tableStructure.Columns)
        {
            createScript.AppendLine($"{column.ColumnName} ");
            AppendTableColumnDataType(createScript, column);
            AppendTableColumnPrimaryKey(createScript, column);
            AppendTableColumnNull(createScript, column);
            createScript.Append(',');
            AppendTableColumnFK(createScript, column);
        }
        createScript.AppendLine(");");
        return createScript.ToString();
    }

    private void AppendTableColumnDataType(StringBuilder createScript, TableColumnInfo column)
    {
        string[] variableTypes = {"binary", "varbinary", "char", "nchar", "varchar", "nvarchar" };
        if (variableTypes.Contains(column.DataType))
        {
            createScript.Append($"{column.DataType} ({column.MaxLength}) ");
        }
        else
        {
            createScript.Append($"{column.DataType} ");
        }
    }
    private void AppendTableColumnPrimaryKey(StringBuilder createScript, TableColumnInfo column)
    {
        if (column.IsPrimaryKey ?? false)
        {
            createScript.Append("PRIMARY KEY ");
        }
        if (column.IsIdentity ?? false)
        {
            createScript.Append("IDENTITY (1, 1) ");
        }
    }

    private void AppendTableColumnNull(StringBuilder createScript, TableColumnInfo column)
    {
        if (column.IsAllowNulls ?? false)
        {
            createScript.Append("NULL");
        }
        else
        {
            createScript.Append("NOT NULL");
        }
    }

    private void AppendTableColumnFK(StringBuilder createScript, TableColumnInfo column)
    {
        if (column.IsForeignKey ?? false)
        {
            createScript.AppendLine($"FOREIGN KEY ({column.ColumnName}) " +
                $"REFERENCES [{column.RelatedTableSchema}].[{column.RelatedTable}] ({column.RelatedTableColumn}),");
        }
    }

    private async Task<QueryResultTable> GetAllReferencedFkTablesScriptAsync(ApplyChangesDto applyChangesDto, string tableSchema, string tableName)
    {
        _queryParameters = RegisterQuery();
        var script = $@"SELECT name [FkName], 
	                   OBJECT_SCHEMA_NAME(parent_object_id) + '.' + OBJECT_NAME(parent_object_id) [ChildTable]
                       FROM sys.foreign_keys 
                       WHERE OBJECT_SCHEMA_NAME(referenced_object_id) = '{tableSchema}' AND 
                       OBJECT_NAME(referenced_object_id) = '{tableName}'";
        var formattedScript = _sqlFormatterService.GetFormattedSql(applyChangesDto.DbEngine, script);
        await _hubContext.Clients.User(applyChangesDto.ClientId!)
                 .ExecuteScriptAsync(_queryParameters.queryId, formattedScript.Content!);
        return await _queryParameters.tcs.Task;
    }

    private async Task DropAllReferencedFkTablesScriptAsync(ApplyChangesDto applyChangesDto, QueryResultTable queryResultTable)
    {
        _queryParameters = RegisterQuery();
        for (int i = 0; i < queryResultTable.RowCount; i++)
        {
            var dropFkScript = $"ALTER TABLE {queryResultTable.Rows[i][1]} DROP CONSTRAINT {queryResultTable.Rows[i][0]}";
            await ExecuteScriptAsync(applyChangesDto, dropFkScript);
        }
    }

    private async Task ExecuteScriptAsync(ApplyChangesDto applyChangesDto, string script)
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
