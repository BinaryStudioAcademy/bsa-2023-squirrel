namespace Squirrel.Core.BLL.Interfaces;

public interface IChangeRecordService
{
    Task<Guid> AddChangeRecordAsync();
}