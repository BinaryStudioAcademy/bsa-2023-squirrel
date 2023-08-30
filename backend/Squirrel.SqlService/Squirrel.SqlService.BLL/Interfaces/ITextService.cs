using Squirrel.Shared.DTO.Text;

namespace Squirrel.SqlService.BLL.Interfaces;

public interface ITextService
{
    InLineDiffResultDto GetInlineDiffs(TextPairRequestDto textPairDto);
    SideBySideDiffResultDto GetSideBySideDiffs(TextPairRequestDto textPairDto);
}