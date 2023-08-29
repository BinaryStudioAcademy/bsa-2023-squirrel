using Squirrel.Core.Common.DTO.Text;

namespace Squirrel.SqlService.WebApi.Interfaces;

public interface ITextService
{
    InLineDiffResultDto GetInlineDiffs(TextPairRequestDto textPairDto);
    SideBySideDiffResultDto GetSideBySideDiffs(TextPairRequestDto textPairDto);
}