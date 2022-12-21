using Web.Models;
using Web.Models.Dto.Login;
using Web.Services.IServices;

namespace Web.Services
{
    public class AuthService : BaseService, IAuthService
    {
        private readonly IHttpClientFactory _clientFactory;
        private string villaUrl;

        public AuthService(IHttpClientFactory httpClient, IConfiguration configuration) : base(httpClient)
        {
            _clientFactory = httpClient;
            villaUrl = configuration.GetValue<string>("ServiceUrls:VillaAPI");
        }

        public Task<T> LoginAsync<T>(LoginRequestDTO requestDTO)
        {
            return SendAsync<T>(new ApiRequest() { ApiType = StaticDetails.ApiType.POST, Data = requestDTO, Url = villaUrl + "/api/User/Login"});
        }

        public Task<T> RegisterAsync<T>(RegisterationRequestDTO requestDTO)
        {
            return SendAsync<T>(new ApiRequest() { ApiType = StaticDetails.ApiType.POST, Data = requestDTO, Url = villaUrl + "/api/User/Register" });
        }
    }
}
