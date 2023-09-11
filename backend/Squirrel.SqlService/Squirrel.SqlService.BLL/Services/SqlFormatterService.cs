using Squirrel.SqlService.BLL.Interfaces;
using gudusoft.gsqlparser;
using gudusoft.gsqlparser.pp.para;
using gudusoft.gsqlparser.pp.stmtformatter;
using Squirrel.Core.DAL.Enums;
using Squirrel.Shared.Exceptions;
using Microsoft.SqlServer.TransactSql.ScriptDom;
using System.Collections.Generic;

namespace Squirrel.SqlService.BLL.Services;

public class SqlFormatterService : ISqlFormatterService
{
    private EDbVendor _dbVendor;
    private TGSqlParser? _parser;

    //Using old dll
    public string GetFormattedSql(string inputSQL, DbEngine dbEngine)
    {
        if (HasSyntaxError(inputSQL, dbEngine, out string error))
        {
            throw new SqlSyntaxException(error);
        };
        GFmtOpt option = GFmtOptFactory.newInstance();
        return FormatterFactory.pp(_parser, option);
    }

    //Using old dll
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
    public string Format(string inputSQL)
    {
        Sql160ScriptGenerator scriptGenerator = new Sql160ScriptGenerator(GetFormattingOptions());
        TSql160Parser parser = new TSql160Parser(true);
        TSqlFragment fragment;
        IList<ParseError> errors = new List<ParseError>();
        try
        {
            fragment = parser.Parse(new StringReader(inputSQL), out errors);
            if (errors.Count > 0)
            {
                throw new SqlSyntaxException(string.Join("\n", errors.Select(
                    e => $"Syntax error [{e.Number}]: {e.Message}. Position({e.Line}, {e.Column})")));
            }
            scriptGenerator.GenerateScript(fragment, out string result);
            return result;
        }
        catch (Exception)
        {
            throw new SqlSyntaxException(string.Join("\n", errors.Select(
                e => $"Syntax error [{e.Number}]: {e.Message}. Position({e.Line}, {e.Column})")));
        }
    }
    private SqlScriptGeneratorOptions GetFormattingOptions()
    {
        var options = new SqlScriptGeneratorOptions();
        options.SqlEngineType = SqlEngineType.All;
        options.IncludeSemicolons = true;
        options.AlignClauseBodies = true;
        options.AlignColumnDefinitionFields = true;
        options.AlignSetClauseItem = true;
        options.AsKeywordOnOwnLine = true;
        options.IndentationSize = 10;
        options.IndentSetClause = true;
        options.IndentViewBody = true;
        options.KeywordCasing = KeywordCasing.Uppercase;
        options.MultilineInsertSourcesList = true;
        options.MultilineInsertTargetsList = true;
        options.MultilineSelectElementsList = true;
        options.MultilineViewColumnsList = true;
        options.MultilineWherePredicatesList = true;
        return options;
    }
}
