using Squirrel.Core.DAL.Entities;

namespace Squirrel.Core.BLL.Interfaces;

public interface IDBStructureSaverService
{
    Task SaveDBStructureToAzureBlob(ChangeRecord changeRecord);
}