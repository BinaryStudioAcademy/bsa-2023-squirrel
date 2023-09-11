using Squirrel.Core.DAL.Enums;

namespace Squirrel.SqlService.BLL.Interfaces;

public interface ISqlFormatterService
{
    string GetFormattedSql(string originalSQL, DbEngine dbEngine);
    bool HasSyntaxError(string inputSQL, DbEngine dbEngine, out string errorMessage);
    string Format(string inputSQL);
}
