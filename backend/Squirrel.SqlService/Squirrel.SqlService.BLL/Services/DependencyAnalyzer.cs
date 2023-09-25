using System.Text.RegularExpressions;
using Squirrel.SqlService.BLL.Interfaces;

namespace Squirrel.SqlService.BLL.Services;

public class DependencyAnalyzer : IDependencyAnalyzer
{
    public ICollection<string> AnalyzeDependencies(string spContent, List<string>? objectList = null)
    {
        var references = FindReferences(spContent);

        if (objectList is null)
        {
            return references;
        }

        var dependencies = new List<string>();

        foreach (string reference in references)
        {
            if (objectList.Contains(reference))
            {
                dependencies.Add(reference);
            }
        }

        return dependencies;
    }

    private ICollection<string> FindReferences(string spContent)
    {
        var references = new List<string>();

        // Used regular expressions to find references
        var spRegex = new Regex(
           @"(?<=\execute|exec\s+)(?:(?:([#@_a-z][#@_$\.0-9a-z]*)(?!\]))|[\[]([#@_a-z][#@_$\.0-9a-z\s]*)[\]])",
            RegexOptions.IgnoreCase);

        var functionRegex = new Regex(
            @"\b\w+\.\w+\s*(?=\()",
            RegexOptions.IgnoreCase);

        // Got matches from both regex patterns
        var spMatches = spRegex.Matches(spContent);
        var functionMatches = functionRegex.Matches(spContent);

        // Combined the results into a single list
        references.AddRange(spMatches.Concat(functionMatches).Select(m => m.Value));

        return references;
    }
}
