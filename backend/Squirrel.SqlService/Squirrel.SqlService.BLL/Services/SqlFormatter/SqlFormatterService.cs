using Squirrel.SqlService.BLL.Interfaces;
using Squirrel.Shared.Exceptions;
using Microsoft.SqlServer.TransactSql.ScriptDom;
using System.Diagnostics;
using Squirrel.Core.DAL.Enums;
using Squirrel.Core.Common.DTO.Script;

namespace Squirrel.SqlService.BLL.Services.SqlFormatter;

public class SqlFormatterService : ISqlFormatterService
{
    private readonly string _pythonExePath;
    public SqlFormatterService(string pythonExePath)
    {
        _pythonExePath = pythonExePath;
    }
    public ScriptContentDto GetFormattedSql(DbEngine dbEngine, string inputSql)
    {
        Validate(inputSql);

        return dbEngine switch
        {
            DbEngine.SqlServer => FormatMsSqlServer(inputSql),
            DbEngine.PostgreSql => FormatPostgreSql(inputSql),
            _ => throw new NotImplementedException($"Database type {dbEngine} is not supported."),
        };
    }
    private ScriptContentDto FormatMsSqlServer(string inputSql)
    {
        var scriptGenerator = new Sql160ScriptGenerator(GetFormattingOptions());

        var fragment = new TSql160Parser(true).Parse(new StringReader(inputSql), out IList<ParseError> errors);
        if (errors.Count > 0)
        {
            throw new SqlSyntaxException(CreateErrorMessage(errors));
        }

        scriptGenerator.GenerateScript(fragment, out var result);
        return new ScriptContentDto { Content = result };
    }

    private ScriptContentDto FormatPostgreSql(string inputSql)
    {
        var assemblyPath = typeof(SqlFormatterService).Assembly.Location.Split('\\').SkipLast(1);
        var filePath = string.Join("\\", assemblyPath) + "\\Services\\SqlFormatter\\PgSqlParser.py";

        string argsFile = string.Format("{0}\\{1}.txt", Path.GetDirectoryName(filePath.ToString()), Guid.NewGuid());

        ProcessStartInfo startInfo = new ProcessStartInfo
        {
            FileName = _pythonExePath,
            UseShellExecute = false,
            RedirectStandardOutput = true,
            CreateNoWindow = false,
            RedirectStandardError = true
        };

        string result = string.Empty;

        try
        {
            using (StreamWriter sw = new StreamWriter(argsFile))
            {
                sw.WriteLine(inputSql);
                startInfo.Arguments = string.Format(
                    "{0} {1}", string.Format(@"""{0}""", filePath), string.Format(@"""{0}""", argsFile));
            }
            PythonScriptExecutor.ExecuteScript(startInfo, out result);
        }
        finally
        {
            File.Delete(argsFile);
        }
        return new ScriptContentDto { Content = result };
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
            IndentationSize = 4,
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

    private void Validate(string inputSql)
    {
        if (string.IsNullOrEmpty(inputSql) || string.IsNullOrWhiteSpace(inputSql))
        {
            throw new SqlSyntaxException("Input SQL cannot be Null or Empty");
        }
    }

    private string CreateErrorMessage(IList<ParseError> errors)
    {
        return string.Join("\n", errors.Where(e => !e.Message.Contains("An internal parser error occurred"))
            .Select(e => $"Syntax error [{e.Number}]: {e.Message}. Position({e.Line}, {e.Column})"));
    }
}
