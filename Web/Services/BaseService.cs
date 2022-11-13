using Newtonsoft.Json;
using System.Text;
using Web.Models;
using Web.Services.IServices;

namespace Web.Services
{
    public class BaseService : IBaseService
    {
        public ApiResponse ApiResponse { get; set; }
        public IHttpClientFactory HttpClient { get; set; }
        public BaseService(IHttpClientFactory httpClient)
        {
            HttpClient = httpClient;
            ApiResponse = new ApiResponse();
        }

        public async Task<T> SendAsync<T>(ApiRequest apiRequest)
        {
            try
            {
                var client = HttpClient.CreateClient();
                HttpRequestMessage message = new HttpRequestMessage();
                message.Headers.Add("Accept", "application/json");
                message.RequestUri = new Uri(apiRequest.Url);
                if (apiRequest.Data != null)
                {
                    message.Content = new StringContent(JsonConvert.SerializeObject(apiRequest.Data), Encoding.UTF8, "application/json");
                }
                switch (apiRequest.ApiType)
                {
                    case StaticDetails.ApiType.POST:
                        {
                            message.Method = HttpMethod.Post;
                        }
                        break;
                    case StaticDetails.ApiType.PUT:
                        {
                            message.Method= HttpMethod.Put;
                        }
                        break;
                    case StaticDetails.ApiType.DELETE:
                        {
                            message.Method = HttpMethod.Delete;
                        }
                        break;
                    default:
                        {
                            message.Method = HttpMethod.Get;
                        }
                        break;
                }

                HttpResponseMessage apiResponse = null;
                apiResponse = await client.SendAsync(message);
                var apiContent = await apiResponse.Content.ReadAsStringAsync();
                var finalResult = JsonConvert.DeserializeObject<T>(apiContent);
                return finalResult;
            }
            catch (Exception ex)
            {
                var dto = new ApiResponse
                {
                    Messages = new List<string> { ex.Message },
                    IsSuccess = false
                };
                var res = JsonConvert.SerializeObject(dto);
                var finalResult = JsonConvert.DeserializeObject<T>(res);
                return finalResult;
            }
        }
    }
}
