namespace Squirrel.Shared.Enums;

public enum ErrorType
{
    InvalidEmail = 1,
    InvalidUsername,
    InvalidPassword,
    InvalidEmailOrPassword,
    NotFound,
    Internal,
    InvalidToken,
    InvalidProject,
    LargeFile,
    InvalidFileFormat,
    RefreshTokenExpired,
}