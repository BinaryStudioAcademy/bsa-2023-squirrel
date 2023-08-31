﻿using Squirrel.SqlService.BLL.Interfaces;
using System.Text.RegularExpressions;

namespace Squirrel.SqlService.BLL.Services;

public class DependencyAnalyzer : IDependencyAnalyzer
{
    public List<string> AnalyzeDependencies(string spContent, List<string>? objectList = null)
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

    private List<string> FindReferences(string spContent)
    {
        var references = new List<string>();

        // Used regular expressions to find references
        Regex spRegex = new Regex(
           @"(?<=\execute|exec\s+)(?:(?:([#@_a-z][#@_$\.0-9a-z]*)(?!\]))|[\[]([#@_a-z][#@_$\.0-9a-z\s]*)[\]])",
            RegexOptions.IgnoreCase);

        Regex functionRegex = new Regex(
            @"\b\w+\.\w+\s*(?=\()",
            RegexOptions.IgnoreCase);

        // Got matches from both regex patterns
        MatchCollection spMatches = spRegex.Matches(spContent);
        MatchCollection functionMatches = functionRegex.Matches(spContent);

        // Combined the results into a single list
        references.AddRange(spMatches.Concat(functionMatches).Select(m => m.Value));

        return references;
    }
}