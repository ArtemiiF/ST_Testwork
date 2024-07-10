using Microsoft.AspNetCore.Mvc;
using ST_Testwork.Models;
using System.Net.Http;
using System.Threading.Tasks;

namespace ST_Testwork.Interfaces
{
    public interface IHttpClientService
    {
        HttpActionResponse<T> Get<T>(string endpoint) where T : class;

        Task<HttpActionResponse<T>> GetAsync<T>(string endpoint) where T : class;
    }
}
