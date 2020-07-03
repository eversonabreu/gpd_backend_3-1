using GpdFrontEnd.Infraestructure;
using System.Net.Http;
using System.Threading.Tasks;
using System.Text.Json;

namespace GpdFrontEnd.Services.System
{
    public class HttpService
    {
        private readonly HttpClient httpClient;
        private readonly Session session;

        public HttpService(HttpClient httpClient, Session session)
        {
            this.httpClient = httpClient;
            this.session = session;
        }

        public async Task<HttpClient> GetHttpAsync()
        {
            httpClient.DefaultRequestHeaders.Clear();
            string token = await session.GetTokenAsync();

            if (token is null)
            {
                return null;
            }

            httpClient.DefaultRequestHeaders.Add("Authorization", $"Bearer {token}");
            return httpClient;
        }

        public async Task<ResultSet<TResult>> GetResultSetAsync<TResult>(HttpResponseMessage response, bool propertyNameCaseInsensitive = true)
        {
            var content = await response.Content.ReadAsStringAsync();
            var result = JsonSerializer.Deserialize<ResultSet<TResult>>(content, new JsonSerializerOptions { PropertyNameCaseInsensitive = propertyNameCaseInsensitive });
            return result;
        }

        public async Task<TResult> GetObjectAsync<TResult>(HttpResponseMessage response, bool propertyNameCaseInsensitive = true)
        {
            var content = await response.Content.ReadAsStringAsync();
            var result = JsonSerializer.Deserialize<TResult>(content, new JsonSerializerOptions { PropertyNameCaseInsensitive = propertyNameCaseInsensitive });
            return result;
        }
    }
}
