using Squirrel.Core.DAL.Entities;
using Squirrel.Shared.DTO.DatabaseItem;

namespace Squirrel.Core.BLL.Interfaces;

public interface IDBStructureSaverService
{
    Task<ICollection<DatabaseItem>> SaveDBStructureToAzureBlob(ChangeRecord changeRecord, Guid clientId);
}