using Squirrel.Core.DAL.Entities;

namespace Squirrel.Core.BLL.Interfaces;

public interface IDbStructureSaverService
{
    Task SaveDBStructureToAzureBlobAsync(ChangeRecord changeRecord, Guid clientId);
}