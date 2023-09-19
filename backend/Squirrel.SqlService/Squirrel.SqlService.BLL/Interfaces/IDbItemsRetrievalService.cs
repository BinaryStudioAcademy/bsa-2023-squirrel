using Squirrel.Shared.DTO.DatabaseItem;
using Squirrel.SqlService.BLL.Models.DTO;

namespace Squirrel.SqlService.BLL.Interfaces;

public interface IDbItemsRetrievalService
{
    Task<ICollection<DatabaseItem>> GetAllItemsAsync(Guid clientId);

    Task<DbStructureDto> GetAllDbStructureAsync(Guid clientId);
}

