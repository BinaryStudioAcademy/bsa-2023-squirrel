using Squirrel.Shared.DTO.DatabaseItem;

namespace Squirrel.Core.BLL.Interfaces;

public interface IChangeRecordService
{
    Task<ICollection<DatabaseItem>> AddChangeRecordAsync(Guid clientId);
}