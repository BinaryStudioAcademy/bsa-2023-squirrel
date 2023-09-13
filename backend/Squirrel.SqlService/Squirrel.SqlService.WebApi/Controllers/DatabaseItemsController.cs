using Microsoft.AspNetCore.Mvc;
using Squirrel.Shared.DTO.DatabaseItem;
using Squirrel.SqlService.BLL.Interfaces;

namespace Squirrel.SqlService.WebApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public sealed class DatabaseItemsController : ControllerBase
{
    private readonly IDbItemsRetrievalService _dbItemsRetrieval;
    private readonly ISqlFormatterService _sqlFormatter;

    public DatabaseItemsController(IDbItemsRetrievalService dbItemsRetrieval, ISqlFormatterService sqlFormatter)
    {
        _dbItemsRetrieval = dbItemsRetrieval;
        _sqlFormatter = sqlFormatter;
    }

    [HttpGet]
    public ActionResult<List<DatabaseItem>> GetAllItems()
    {
        return Ok(_dbItemsRetrieval.GetAllItems());
    }

    [HttpGet("formatsql")]
    public ActionResult<string> GetFormattedSql()
    {
        string sql = @"
     SELECT inventory_id
     FROM inventory
     WHERE film_id = $1
     AND store_id = $2
     AND inventory_in_stock(inventory_id);
";
        return Ok(_sqlFormatter.FormatPostgreSql(sql));
    }
}
