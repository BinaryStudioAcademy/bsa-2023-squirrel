using Squirrel.Core.DAL.Enums;

namespace Squirrel.SqlService.BLL.Interfaces;

public interface ISqlFormatterService
{
    string GetFormattedSql(DbEngine dbEngine, string inputSql);
}
