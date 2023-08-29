using Squirrel.Shared.Enums;

namespace Squirrel.Shared.DTO.Error;

public record ErrorDetailsDto(string Message, ErrorType ErrorType);
