using Main.Models.Dto.Login;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace Main.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ApiBaseController
    {
        public UserController(IServiceProvider provider) : base(provider) { }

        [HttpPost]
        public async Task<IActionResult> Login([FromBody] LoginRequestDTO model)
        {
            var loginResponse = await _unitOfWork.User.Login(model);
            if (loginResponse.User == null || string.IsNullOrEmpty(loginResponse.Token))
            {
                _response.IsSuccess = false;
                _response.StatusCode = HttpStatusCode.BadRequest;
                _response.Messages.Add("Username or password is error");
                return BadRequest(_response);
            }
            _response.IsSuccess = true;
            _response.StatusCode = HttpStatusCode.OK;
            _response.Result= loginResponse;
            return Ok(_response);
        }
    }
}
