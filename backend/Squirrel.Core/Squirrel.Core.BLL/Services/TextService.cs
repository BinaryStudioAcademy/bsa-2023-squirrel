using DiffPlex.DiffBuilder;
using DiffPlex;
using Squirrel.Core.BLL.Interfaces;
using Squirrel.Core.Common.DTO.Auth;

namespace Squirrel.Core.BLL.Services;

public class TextService : ITextService
{
    public List<DiffLineResult> CompareTwoTexts(string oldText, string newText)
    {
        var inlineBuilder = new InlineDiffBuilder(new Differ());
        var inlineDiff = inlineBuilder.BuildDiffModel(oldText, newText, ignoreWhitespace: false);

        var result = new List<DiffLineResult>();

        foreach (var line in inlineDiff.Lines)
        {
            var diffLine = new DiffLineResult
            {
                Text = line.Text,
                Type = line.Type
            };

            result.Add(diffLine);
        }

        return result;
    }
}
