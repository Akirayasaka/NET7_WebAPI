using Web.Models.Dto.Login;

namespace Web.Services.IServices
{
    public interface IAuthService
    {
        Task<T> LoginAsync<T>(LoginRequestDTO requestDTO);
        Task<T> RegisterAsync<T>(RegisterationRequestDTO requestDTO);
    }
}
