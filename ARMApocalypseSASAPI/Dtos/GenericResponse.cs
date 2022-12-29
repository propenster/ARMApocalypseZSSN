using System.Net;

namespace ARMApocalypseSASAPI.Dtos
{
    public class GenericResponse<T>
    {
        public T Data { get; set; }
        public string Message { get; set; } = string.Empty;
        public bool Success { get; set; }
        public HttpStatusCode StatusCode { get; set; }
    }
}
