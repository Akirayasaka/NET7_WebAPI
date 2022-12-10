using Main.Models;
using Main.Models.Dto.Login;

namespace Main.Repository.IRepository
{
    public interface IUserRepository : IRepository<User>
    {
        bool IsUniqueUser(string username);
        Task<LoginResponseDTO> Login(LoginRequestDTO loginRequestDTO);
        Task<User> Register(RegisterationRequestDTO registerationRequestDTO);
    }
}
