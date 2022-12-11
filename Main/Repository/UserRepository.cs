using Main.Data;
using Main.Models;
using Main.Models.Dto.Login;
using Main.Repository.IRepository;

namespace Main.Repository
{
    public class UserRepository : Repository<User>, IUserRepository
    {
        private readonly ApplicationDbContext _db;
        private string secretKey;
        public UserRepository(ApplicationDbContext db, IConfiguration configuration) : base(db)
        {
            _db = db;
            secretKey = configuration.GetValue<string>("ApiSettings:Secret");
        }

        public bool IsUniqueUser(string username)
        {
            var user = _db.Users.FirstOrDefault(x => x.UserName == username);
            return user == null;
        }

        public async Task<LoginResponseDTO> Login(LoginRequestDTO loginRequestDTO)
        {
            var user = _db.Users.FirstOrDefault(x => x.UserName.ToLower() == loginRequestDTO.UserName.ToLower() && x.Password == loginRequestDTO.Password);
            if (user == null)
            {
                return null;
            }

            // if user was found, generate JWT Token

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
