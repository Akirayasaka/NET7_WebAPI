using Main.Data;
using Main.Models;
using Main.Models.Dto.Login;
using Main.Repository.IRepository;

namespace Main.Repository
{
    public class UserRepository : Repository<User>, IUserRepository
    {
        private readonly ApplicationDbContext _db;
        public UserRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }

        public bool IsUniqueUser(string username)
        {
            var user = _db.Users.FirstOrDefault(x => x.UserName == username);
            return user == null;
        }

        public async Task<LoginResponseDTO> Login(LoginRequestDTO loginRequestDTO)
        {
            throw new NotImplementedException();
        }

        public async Task<User> Register(RegisterationRequestDTO registerationRequestDTO)
        {
            User user = new()
            {
                UserName = registerationRequestDTO.UserName,
                Password = registerationRequestDTO.Password,
                Name = registerationRequestDTO.Name,
                Role = registerationRequestDTO.Role
            };

            _db.Users.Add(user);
            _db.SaveChanges();
            user.Password = "";
            return user;
        }
    }
}
