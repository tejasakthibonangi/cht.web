namespace Cht.HMS.Web.UI.Factory
{
    public interface IRepositoryFactory
    {
        Task<TResponse> SendAsync<TResponse>(HttpMethod method, string uri);
        Task<TResponse> SendAsync<TRequest, TResponse>(HttpMethod method, string uri, TRequest entity = default);

    }
}
