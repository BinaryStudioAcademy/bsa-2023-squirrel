using Squirrel.Core.Common.DTO.Text;

namespace Squirrel.Core.BLL.Interfaces;

public interface ITextService
{
    InLineDiffResultDto GetInlineDiffs(CompareTextsDto compareTextsDto);
    SideBySideDiffResultDto GetSideBySideDiffs(CompareTextsDto compareTextsDto);
}