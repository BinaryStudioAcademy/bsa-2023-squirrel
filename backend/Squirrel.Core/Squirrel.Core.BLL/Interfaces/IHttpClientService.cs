namespace Squirrel.Core.BLL.Interfaces
{
    public interface IHttpClientService
    {
        Task<TResponse> GetAsync<TResponse>(string requestUrl);
        Task<HttpResponseMessage> SendAsync(HttpRequestMessage request);
    }
}
