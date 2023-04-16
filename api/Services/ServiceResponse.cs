using System.Net;

namespace api.Services
{
    public class ServiceResponse<T> where T : class
    {
        public T? Data { get; set; }
        public bool Success { get; set; } = true;
        public string? Message { get; set; }
        public string? Error { get; set; }
    }
}