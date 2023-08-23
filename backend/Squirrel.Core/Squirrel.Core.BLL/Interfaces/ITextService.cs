using Squirrel.Core.Common.DTO.Text;

namespace Squirrel.Core.BLL.Interfaces;

public interface ITextService
{
    InLineDiffResultDto GetInlineDiffs(TextPairRequestDto textPairDto);
    SideBySideDiffResultDto GetSideBySideDiffs(TextPairRequestDto textPairDto);
}