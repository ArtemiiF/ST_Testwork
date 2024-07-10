using ST_Testwork.Interfaces;
using ST_Testwork.Models;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;

namespace ST_Testwork.Services
{
    public class HttpClientService : IHttpClientService
    {
        private readonly HttpClient httpClient;

        public HttpClientService(HttpClient httpClient)
        {
            this.httpClient = httpClient;
        }

        public HttpActionResponse<T> Get<T>(string endpoint) where T : class
        {
            return GetAsync<T>(endpoint).GetAwaiter().GetResult();
        }

        public async Task<HttpActionResponse<T>> GetAsync<T>(string endpoint) where T : class
        {
            HttpResponseMessage httpResponseMessage = await httpClient.GetAsync(endpoint);
            if (!httpResponseMessage.IsSuccessStatusCode)
            {
                return new HttpActionResponse<T>()
                {
                    IsSuccessStatusCode = httpResponseMessage.IsSuccessStatusCode,
                    StatusCode = httpResponseMessage.StatusCode
                };
            }

            var strContent = await httpResponseMessage.Content.ReadAsStringAsync();
            var result = JsonSerializer.Deserialize<T>(strContent);
            return new HttpActionResponse<T>()
            {
                Value = result,
                IsSuccessStatusCode = httpResponseMessage.IsSuccessStatusCode,
                StatusCode = httpResponseMessage.StatusCode
            };
        }
    }
}
