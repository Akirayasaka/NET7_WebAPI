using Microsoft.Extensions.Configuration;
using Web.Models;
using Web.Models.Dto.Villa;
using Web.Services.IServices;

namespace Web.Services
{
    public class VillaService : BaseService, IVillaService
    {
        private readonly IHttpClientFactory _clientFactory;
        private string villaUrl;

        public VillaService(IHttpClientFactory clientFactory, IConfiguration configuration): base(clientFactory)
        {
            _clientFactory = clientFactory;
            villaUrl = configuration.GetValue<string>("ServiceUrls:VillaAPI");
        }

        public Task<T> CreateAsync<T>(VillaCreateDTO dto)
        {
            return SendAsync<T>(new ApiRequest()
            {
                ApiType = StaticDetails.ApiType.POST,
                Data = dto,
                Url = villaUrl + "/api/villaAPI"
            });
        }

        public Task<T> DeleteAsync<T>(int id)
        {
            return SendAsync<T>(new ApiRequest()
            {
                ApiType= StaticDetails.ApiType.DELETE,
                Url = villaUrl + "/api/villaAPI" + id
            });
        }

        public Task<T> GetAllAsync<T>()
        {
            return SendAsync<T>(new ApiRequest()
            {
                ApiType = StaticDetails.ApiType.GET,
                Url = villaUrl + "/api/villaAPI"
            });
        }

        public Task<T> GetAsync<T>(int id)
        {
            return SendAsync<T>(new ApiRequest()
            {
                ApiType = StaticDetails.ApiType.GET,
                Url = villaUrl + "/api/villaAPI" + id
            });
        }

        public Task<T> UpdateAsync<T>(VillaUpdateDTO dto)
        {
            return SendAsync<T>(new ApiRequest()
            {
                ApiType = StaticDetails.ApiType.PUT,
                Data = dto,
                Url = villaUrl + "/api/villaAPI" + dto.Id
            });
        }
    }
}
