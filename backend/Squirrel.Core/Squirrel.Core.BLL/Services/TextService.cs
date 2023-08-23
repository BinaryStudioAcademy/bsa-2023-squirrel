using DiffPlex.DiffBuilder;
using DiffPlex;
using Squirrel.Core.BLL.Interfaces;
using Squirrel.Core.Common.DTO.Text;

namespace Squirrel.Core.BLL.Services;

public class TextService : ITextService
{
    public InLineDiffResultDto GetInlineDiffs(CompareTextsDto compareTextsDto)
    {
        var inlineBuilder = new InlineDiffBuilder(new Differ());
        var inlineDiff = inlineBuilder.BuildDiffModel(compareTextsDto.OldText, compareTextsDto.NewText, compareTextsDto.IgnoreWhitespace);

        var result = inlineDiff.Lines.Select(line => new DiffLineResult
        {
            Text = line.Text,
            Type = line.Type,
            Position = line.Position.HasValue ? line.Position.Value : -1,
            SubPieces = line.SubPieces
        }).ToList();

        return new InLineDiffResultDto { HasDifferences = inlineDiff.HasDifferences, DiffLinesResults = result };
    }

    public SideBySideDiffResultDto GetSideBySideDiffs(CompareTextsDto compareTextsDto)
    {
        var sbsBuilder = new SideBySideDiffBuilder(new Differ());
        var sbsDiff = sbsBuilder.BuildDiffModel(compareTextsDto.OldText, compareTextsDto.NewText, compareTextsDto.IgnoreWhitespace);

        var result = new SideBySideDiffResultDto
        {
            HasDifferences = sbsDiff.OldText.HasDifferences || sbsDiff.NewText.HasDifferences,

            OldTextLines = sbsDiff.OldText.Lines.Select(line => new DiffLineResult
            {
                Text = line.Text,
                Type = line.Type,
                Position = line.Position.HasValue ? line.Position.Value : -1
            }).ToList(),

            NewTextLines = sbsDiff.NewText.Lines.Select(line => new DiffLineResult
            {
                Text = line.Text,
                Type = line.Type,
                Position = line.Position.HasValue ? line.Position.Value : -1
            }).ToList()
        };

        return result;
    }
}
