using Squirrel.Shared.DTO.DatabaseItem;

namespace Squirrel.Core.BLL.Interfaces;

public interface IDatabaseItemsService
{
    Task<List<DatabaseItem>> GetAllItems();
}