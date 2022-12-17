using Main.Models;
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

        [HttpPost("Login")]
        public async Task<ActionResult<ApiResponse>> Login([FromBody] LoginRequestDTO model)
        {
            LoginResponseDTO loginResponse = await _unitOfWork.User.Login(model);
            if (loginResponse.User == null || string.IsNullOrEmpty(loginResponse.Token))
            {
                _response.IsSuccess = false;
                _response.StatusCode = HttpStatusCode.BadRequest;
                _response.Messages.Add("Username or password is error");
                return BadRequest(_response);
            }
            _response.IsSuccess = true;
            _response.StatusCode = HttpStatusCode.OK;
            _response.Result = loginResponse;
            return Ok(_response);
        }

        [HttpPost("Register")]
        public async Task<ActionResult<ApiResponse>> Register([FromBody] RegisterationRequestDTO model)
        {
            bool userExsist = _unitOfWork.User.IsUniqueUser(model.UserName);
            if (!userExsist)
            {
                _response.IsSuccess = false;
                _response.StatusCode = HttpStatusCode.BadRequest;
                _response.Messages.Add("Username already exists");
                return BadRequest(_response);
            }

            User user = await _unitOfWork.User.Register(model);
            if (user == null)
            {
                _response.IsSuccess = false;
                _response.StatusCode = HttpStatusCode.BadRequest;
                _response.Messages.Add("Register Error");
                return BadRequest(_response);
            }
            _response.IsSuccess = true;
            _response.StatusCode = HttpStatusCode.OK;
            return Ok(_response);
        }
    }
}
