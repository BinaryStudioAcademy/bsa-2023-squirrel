using Squirrel.Shared.DTO.DatabaseItem;

namespace Squirrel.SqlService.BLL.Interfaces;

public interface IDbItemsRetrievalService
{
    ICollection<DatabaseItem> GetAllItems();
}

