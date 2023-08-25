using Squirrel.Core.BLL.Interfaces;
using System.Text.RegularExpressions;

namespace Squirrel.Core.BLL.Services
{
    public class DependencyAnalyzer : IDependencyAnalyzer
    {
        private List<string> FindReferences(string spContent)
        {
            List<string> references = new List<string>();

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
            List<Match> matches = new List<Match>();
            matches.AddRange(spMatches);
            matches.AddRange(functionMatches);

            references.AddRange(matches.Select(m => m.Value));

            return references;
        }

        public List<string> AnalyzeDependencies(string spContent, List<string>? objectList = null)
        {
            List<string> references = FindReferences(spContent);

            if (objectList != null)
            {
                List<string> dependencies = new List<string>();

                foreach (string reference in references)
                {
                    if (objectList.Contains(reference))
                    {
                        dependencies.Add(reference);
                    }
                }
                return dependencies;

            }
            else
            {
                return references;
            }
        }
    }
}
