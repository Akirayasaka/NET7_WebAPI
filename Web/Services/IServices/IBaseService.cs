using Web.Models;

namespace Web.Services.IServices
{
    public interface IBaseService
    {
        ApiResponse ApiResponse { get; set; }
        Task<T> SendAsync<T>(ApiRequest apiRequest);
    }
}
