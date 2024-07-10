using System.Net;

namespace ST_Testwork.Models
{
    public class HttpActionResponse<T>
    {
        public HttpStatusCode StatusCode { get; set; }

        public T Value { get; set; }

        public bool IsSuccessStatusCode { get; set; }
    }
}
