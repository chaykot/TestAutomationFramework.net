using System.Net;

namespace Entities.API.Entities.ResponseModels
{
    public class ErrorResponse<T>
    {
        public T Message { get; set; }
        public HttpStatusCode Error { get; set; }
        public HttpStatusCode StatusCode { get; set; }
    }
}