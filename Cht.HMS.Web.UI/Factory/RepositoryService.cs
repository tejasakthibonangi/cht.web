using Newtonsoft.Json;
using System.Net;
using System.Text;

namespace Cht.HMS.Web.UI.Factory
{
    public class RepositoryService : IRepositoryService
    {
        private readonly HttpClient _httpClient;

        public RepositoryService(HttpClient httpClient)
        {
            _httpClient = httpClient;
            _httpClient.BaseAddress = new Uri("http://localhost:5054/api/");
            _httpClient.Timeout = new TimeSpan(0, 0, 120);
            _httpClient.DefaultRequestHeaders.Clear();
        }

        public async Task<T> GetAsync<T>(string uri)
        {
            var response = await _httpClient.GetAsync(uri);
            return await HandleResponse<T>(response);
        }

        public async Task<IEnumerable<T>> GetAllAsync<T>(string uri)
        {
            var response = await _httpClient.GetAsync(uri);
            return await HandleResponse<IEnumerable<T>>(response);
        }

        public async Task<TResponse> PostAsync<TRequest, TResponse>(string uri, TRequest entity)
        {
            var content = new StringContent(JsonConvert.SerializeObject(entity), Encoding.UTF8, "application/json");
            var response = await _httpClient.PostAsync(uri, content);
            return await HandleResponse<TResponse>(response);
        }

        public async Task<TResponse> PutAsync<TRequest, TResponse>(string uri, TRequest entity)
        {
            var content = new StringContent(JsonConvert.SerializeObject(entity), Encoding.UTF8, "application/json");
            var response = await _httpClient.PutAsync(uri, content);
            return await HandleResponse<TResponse>(response);
        }

        public async Task<bool> DeleteAsync(string uri)
        {
            var response = await _httpClient.DeleteAsync(uri);
            return await HandleResponse<bool>(response);
        }

        public async Task<bool> BulkInsertAsync<T>(string uri, IEnumerable<T> entities)
        {
            var content = new StringContent(JsonConvert.SerializeObject(entities), Encoding.UTF8, "application/json");
            var response = await _httpClient.PostAsync(uri, content);
            return await HandleResponse<bool>(response);
        }

        private async Task<T> HandleResponse<T>(HttpResponseMessage response)
        {
            if (response.IsSuccessStatusCode)
            {
                if (typeof(T) == typeof(bool) && response.StatusCode == HttpStatusCode.NoContent)
                {
                    return (T)(object)true;
                }

                var content = await response.Content.ReadAsStringAsync();
                var amplifyResponse = JsonConvert.DeserializeObject<T>(content);

                return amplifyResponse;
            }
            return default;
        }
    }
}
