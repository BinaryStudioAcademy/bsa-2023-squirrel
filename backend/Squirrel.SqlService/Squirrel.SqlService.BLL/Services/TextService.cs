using DiffPlex.DiffBuilder;
using DiffPlex;
using Squirrel.Shared.DTO.Text;
using Squirrel.SqlService.BLL.Interfaces;

namespace Squirrel.SqlService.BLL.Services;

public class TextService : ITextService
{
    public InLineDiffResultDto GetInlineDiffs(TextPairRequestDto textPairDto)
    {
        var inlineBuilder = new InlineDiffBuilder(new Differ());
        var inlineDiff = inlineBuilder.BuildDiffModel(textPairDto.OldText, textPairDto.NewText, textPairDto.IgnoreWhitespace);

        var result = inlineDiff.Lines.Select(line => new DiffLineResult
        {
            Text = line.Text,
            Type = line.Type,
            Position = line.Position.HasValue ? line.Position.Value : -1,
            SubPieces = line.SubPieces
        }).ToList();

        return new InLineDiffResultDto { HasDifferences = inlineDiff.HasDifferences, DiffLinesResults = result };
    }

    public SideBySideDiffResultDto GetSideBySideDiffs(TextPairRequestDto textPairDto)
    {
        var sbsBuilder = new SideBySideDiffBuilder(new Differ());
        var sbsDiff = sbsBuilder.BuildDiffModel(textPairDto.OldText, textPairDto.NewText, textPairDto.IgnoreWhitespace);

        var result = new SideBySideDiffResultDto
        {
            HasDifferences = sbsDiff.OldText.HasDifferences || sbsDiff.NewText.HasDifferences,

            OldTextLines = sbsDiff.OldText.Lines.Select(line => new DiffLineResult
            {
                Text = line.Text,
                Type = line.Type,
                Position = line.Position ?? -1
            }).ToList(),

            NewTextLines = sbsDiff.NewText.Lines.Select(line => new DiffLineResult
            {
                Text = line.Text,
                Type = line.Type,
                Position = line.Position ?? -1
            }).ToList()
        };

        return result;
    }
}
