using Squirrel.Core.Common.DTO.Auth;

namespace Squirrel.Core.BLL.Interfaces;

public interface ITextService
{
    List<DiffLineResult> CompareTwoTexts(string oldText, string newText);
}