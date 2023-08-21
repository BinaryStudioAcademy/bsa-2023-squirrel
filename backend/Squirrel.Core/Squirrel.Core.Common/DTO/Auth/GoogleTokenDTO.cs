namespace Squirrel.Core.Common.DTO.Auth
{
    public class GoogleTokenDTO
    {
        public string Token { get; }
        public int ExpiresIn { get; }

        public GoogleTokenDTO(string token, int expiresIn)
        {
            Token = token;
            ExpiresIn = expiresIn;
        }
    }
}
