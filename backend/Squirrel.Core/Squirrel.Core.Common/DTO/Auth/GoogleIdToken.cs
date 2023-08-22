namespace Squirrel.Core.Common.DTO.Auth
{
    public class GoogleIdToken
    {
        public string IdToken { get; }

        public GoogleIdToken(string token)
        {
            IdToken = token;
        }
    }
}
