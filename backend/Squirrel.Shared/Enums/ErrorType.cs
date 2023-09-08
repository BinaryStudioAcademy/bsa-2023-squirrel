namespace Squirrel.Shared.Enums;

public enum ErrorType
{
    InvalidEmail = 1,
    InvalidUsername,
    InvalidEmailOrPassword,
    NotFound,
    Internal,
    InvalidToken,
    RefreshTokenExpired,
}