using Microsoft.AspNetCore.Mvc;
using Squirrel.Shared.DTO.DatabaseItem;
using Squirrel.SqlService.BLL.Interfaces;

namespace Squirrel.SqlService.WebApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class DatabaseItemsController : ControllerBase
{
    private readonly IDbItemsRetrievalService _dbItemsRetrieval;

    public DatabaseItemsController(IDbItemsRetrievalService dbItemsRetrieval)
    {
        _dbItemsRetrieval = dbItemsRetrieval;
    }

    [HttpGet]
    public ActionResult<List<DatabaseItem>> GetAllItems()
    {
        return Ok(_dbItemsRetrieval.GetAllItems());
    }
}
