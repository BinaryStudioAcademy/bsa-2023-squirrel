using Squirrel.Core.DAL.Enums;

namespace Squirrel.SqlService.BLL.Interfaces;

public interface ISqlFormatterService
{
    string GetFormattedSql(string inputSql, DbEngine dbEngine);
    string FormatMsSqlServer(string inputSQL);
    string FormatPostgreSql(string inputSql);
}
