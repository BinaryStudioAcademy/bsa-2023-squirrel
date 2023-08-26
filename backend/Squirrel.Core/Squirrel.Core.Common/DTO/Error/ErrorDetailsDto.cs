using Squirrel.Core.Common.Enums;

namespace Squirrel.Core.Common.DTO.Error;

public record ErrorDetailsDto(string Message, ErrorType ErrorType);
