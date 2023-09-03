namespace Squirrel.Core.BLL.Interfaces
{
    public interface IHttpInternalService
    {
        Task<TResponse> GetAsync<TResponse>(string requestUrl);
    }
}
