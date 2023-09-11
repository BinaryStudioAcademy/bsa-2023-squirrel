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
        string sql = @"SELECT e.last_name AS name, e.commission_pct comm, e.salary * 12 ""Annual Salary""
                                FROM scott.employees AS e
                                WHERE e.salary > 1000 or 1=1
                                ORDER BY
                                e.first_name,
                                e.last_name;";
        return Ok(sql);
    }
}
