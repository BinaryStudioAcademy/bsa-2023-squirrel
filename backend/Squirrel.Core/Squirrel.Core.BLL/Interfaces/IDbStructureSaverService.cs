using Squirrel.Core.DAL.Entities;

namespace Squirrel.Core.BLL.Interfaces;

public interface IDbStructureSaverService
{
    Task SaveDbStructureToAzureBlobAsync(ChangeRecord changeRecord, Guid clientId);
}