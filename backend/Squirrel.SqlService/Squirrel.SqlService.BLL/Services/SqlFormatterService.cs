using Squirrel.SqlService.BLL.Interfaces;
using gudusoft.gsqlparser;
using gudusoft.gsqlparser.pp.para;
using gudusoft.gsqlparser.pp.stmtformatter;
using Squirrel.Core.DAL.Enums;
using Squirrel.Shared.Exceptions;
using Microsoft.SqlServer.TransactSql.ScriptDom;

namespace Squirrel.SqlService.BLL.Services;

public class SqlFormatterService : ISqlFormatterService
{
    private EDbVendor _dbVendor;
    private TGSqlParser? _parser;

    //Using old dll
    public string GetFormattedSql(string inputSql, DbEngine dbEngine)
    {
        if (HasSyntaxError(inputSql, dbEngine, out var error))
        {
            throw new SqlSyntaxException(error);
        };
        var option = GFmtOptFactory.newInstance();
        return FormatterFactory.pp(_parser, option);
    }

    //Using old dll
    public bool HasSyntaxError(string inputSql, DbEngine dbEngine, out string errorMessage)
    {
        errorMessage = string.Empty;
        MapDbVendor(dbEngine);
        _parser = new TGSqlParser(_dbVendor)
        {
            sqltext = inputSql
        };

        var result = _parser.parse();
        if (result == 0)
        {
            return false;
        }

        errorMessage = _parser.Errormessage;
        return true;
    }

    //Using old dll
    private void MapDbVendor(DbEngine dbEngine)
    {
        _dbVendor = dbEngine switch
        {
            DbEngine.MsSqlServer => EDbVendor.dbvmssql,
            DbEngine.PostgreSql => EDbVendor.dbvpostgresql,
            _ => throw new NotImplementedException($"Database type {dbEngine} is not supported."),
        };
    }

    //Using Microsoft.SqlServer.TransactSql.ScriptDom
    public string Format(string inputSql)
    {
        var scriptGenerator = new Sql160ScriptGenerator(GetFormattingOptions());

        var fragment = new TSql160Parser(true).Parse(new StringReader(inputSql), out IList<ParseError> errors);
        if (errors.Count > 0)
        {
            throw new SqlSyntaxException(CreateErrorMessage(errors));
        }

        scriptGenerator.GenerateScript(fragment, out var result);
        return result;
    }
    private SqlScriptGeneratorOptions GetFormattingOptions()
    {
        return new SqlScriptGeneratorOptions
        {
            SqlEngineType = SqlEngineType.All,
            IncludeSemicolons = true,
            AlignClauseBodies = true,
            AlignColumnDefinitionFields = true,
            AlignSetClauseItem = true,
            AsKeywordOnOwnLine = true,
            IndentationSize = 10,
            IndentSetClause = true,
            IndentViewBody = true,
            KeywordCasing = KeywordCasing.Uppercase,
            MultilineInsertSourcesList = true,
            MultilineInsertTargetsList = true,
            MultilineSelectElementsList = true,
            MultilineViewColumnsList = true,
            MultilineWherePredicatesList = true
        };
    }

    private string CreateErrorMessage(IList<ParseError> errors)
    {
        return string.Join("\n", errors.Where(e => !e.Message.Contains("An internal parser error occurred"))
            .Select(e => $"Syntax error [{e.Number}]: {e.Message}. Position({e.Line}, {e.Column})"));
    }
}
