namespace Cht.HMS.Web.UI.Factory
{
    public interface IRepositoryService
    {
        Task<T> GetAsync<T>(string uri);
        Task<IEnumerable<T>> GetAllAsync<T>(string uri);
        Task<TResponse> PostAsync<TRequest, TResponse>(string uri, TRequest entity);
        Task<TResponse> PutAsync<TRequest, TResponse>(string uri, TRequest entity);
        Task<bool> DeleteAsync(string uri);
        Task<bool> BulkInsertAsync<T>(string uri, IEnumerable<T> entities);
    }
}
