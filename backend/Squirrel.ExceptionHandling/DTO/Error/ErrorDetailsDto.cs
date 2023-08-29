using Squirrel.ExceptionHandling.Enums;

namespace Squirrel.ExceptionHandling.DTO.Error;

public record ErrorDetailsDto(string Message, ErrorType ErrorType);
