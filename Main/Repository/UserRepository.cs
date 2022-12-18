using Main.Data;
using Main.Models;
using Main.Models.Dto.Login;
using Main.Repository.IRepository;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Main.Repository
{
    public class UserRepository : Repository<User>, IUserRepository
    {
        private readonly ApplicationDbContext _db;
        private readonly string secretKey;
        public UserRepository(ApplicationDbContext db, IConfiguration configuration) : base(db)
        {
            _db = db;
            secretKey = configuration.GetValue<string>("ApiSettings:Secret");
        }

        public bool IsUniqueUser(string username)
        {
            User user = _db.Users.FirstOrDefault(x => x.UserName == username);
            return user == null;
        }

        public async Task<LoginResponseDTO> Login(LoginRequestDTO loginRequestDTO)
        {
            User user = _db.Users.FirstOrDefault(x => x.UserName.ToLower() == loginRequestDTO.UserName.ToLower() && x.Password == loginRequestDTO.Password);
            if (user == null)
            {
                return new LoginResponseDTO()
                {
                    Token = string.Empty,
                    User = null
                };
            }

            #region if user was found, generate JWT Token
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(secretKey);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.Name, user.Id.ToString()),
                    new Claim(ClaimTypes.Role, user.Role)
                }),
                Expires = DateTime.UtcNow.AddDays(7),
                SigningCredentials = new(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);
            #endregion

            user.Password = $"********";

            LoginResponseDTO loginResponseDTO = new()
            {
                Token = tokenHandler.WriteToken(token),
                User = user,
            };
            return loginResponseDTO;
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
