using System.Net;

namespace Main.Models
{
    public class ApiResponse
    {
        public HttpStatusCode StatusCode { get; set; }
        public bool IsSuccess { get; set; } = true;
        public List<string> Messages { get; set; } = new List<string>();
        public object Result { get; set; }
    }
}
