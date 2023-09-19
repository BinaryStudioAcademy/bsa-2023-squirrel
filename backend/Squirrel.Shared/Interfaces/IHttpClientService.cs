namespace Squirrel.Shared.Interfaces;

public interface IHttpClientService
{
    Task<TResponse> GetAsync<TResponse>(string requestUrl);
    Task<HttpResponseMessage> SendAsync(HttpRequestMessage request);
    Task<TResponse> PostAsync<TRequest, TResponse>(string requestUrl, TRequest requestData);
    Task PostAsync<TRequest>(string requestUrl, TRequest requestData);
}
