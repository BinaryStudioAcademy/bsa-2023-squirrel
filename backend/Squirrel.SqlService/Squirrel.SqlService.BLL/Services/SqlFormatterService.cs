using Squirrel.SqlService.BLL.Interfaces;
using gudusoft.gsqlparser;
using gudusoft.gsqlparser.pp.para;
using gudusoft.gsqlparser.pp.stmtformatter;
using Squirrel.Core.DAL.Enums;
using Squirrel.Shared.Exceptions;

namespace Squirrel.SqlService.BLL.Services;

public class SqlFormatterService : ISqlFormatterService
{
    private EDbVendor _dbVendor;
    private TGSqlParser? _parser;
    public string GetFormattedSql(string inputSQL, DbEngine dbEngine)
    {
        if (HasSyntaxError(inputSQL, dbEngine, out string error)) 
        {
            throw new SqlSyntaxException(error);
        };
        GFmtOpt option = GFmtOptFactory.newInstance();
        return FormatterFactory.pp(_parser, option);
    }

    public bool HasSyntaxError(string inputSQL, DbEngine dbEngine, out string errorMessage)
    {
        errorMessage = string.Empty;
        MapDbVendor(dbEngine);
        _parser = new TGSqlParser(_dbVendor);
        _parser.sqltext = inputSQL;
        int result = _parser.parse();
        if (result == 0)
        {
            return false;
        }
        errorMessage = _parser.Errormessage;
        return true;
    }

    private void MapDbVendor(DbEngine dbEngine)
    {
        _dbVendor = dbEngine switch
        {
            DbEngine.MsSqlServer => EDbVendor.dbvmssql,
            DbEngine.PostgreSql => EDbVendor.dbvpostgresql,
            _ => throw new NotImplementedException($"Database type {dbEngine} is not supported."),
        };
    }
}
