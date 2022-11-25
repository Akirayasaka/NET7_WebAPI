using Main.Models;
using Main.Models.Dto.VillaNumber;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace Main.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VillaNumberAPIController : ApiBaseController
    {
        public VillaNumberAPIController(IServiceProvider provider) : base(provider) { }


        [HttpGet]
        public async Task<ActionResult<ApiResponse>> GetVillaNumbers()
        {
            try
            {
                IEnumerable<VillaNumber> villaNumberList = await _unitOfWork.VillaNumber.GetAllAsync(includeProperties: "Villa");
                _response.Result = _mapper.Map<List<VillaNumberDTO>>(villaNumberList);
                _response.StatusCode = HttpStatusCode.OK;
                return Ok(_response);
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.Messages.Add(ex.Message);
                _response.StatusCode = HttpStatusCode.BadRequest;
                return BadRequest(_response);
            }
        }

        [HttpGet("{id:int}", Name = "GetVillaNumber")]
        public async Task<ActionResult<ApiResponse>> GetVillaNumber(int id)
        {
            if (id == 0)
            {
                _response.IsSuccess = false;
                _response.StatusCode = HttpStatusCode.BadRequest;
                return BadRequest(_response);
            }

            try
            {
                VillaNumber villaNumber = await _unitOfWork.VillaNumber.GetAsync(filter: x => x.VillaNo == id);
                if (villaNumber == null) { return NotFound(); }
                _response.Result = _mapper.Map<VillaNumberDTO>(villaNumber);
                _response.StatusCode = HttpStatusCode.OK;
                return Ok(_response);
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.Messages.Add(ex.Message);
                _response.StatusCode = HttpStatusCode.BadRequest;
                return BadRequest(_response);
            }
        }

        [HttpPost]
        public async Task<ActionResult<ApiResponse>> CreateVillaNumber([FromBody] VillaNumberCreateDTO createDTO)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    _response.IsSuccess = false;
                    _response.StatusCode = HttpStatusCode.BadRequest;
                    _response.Messages.Add("Invalid Data");
                    return BadRequest(_response);
                }
                if (await _unitOfWork.VillaNumber.GetAsync(x => x.VillaNo == createDTO.VillaNo) != null)
                {
                    ModelState.AddModelError("ErrorMessage", "Villa Number already Exists!");
                    _response.IsSuccess = false;
                    _response.StatusCode = HttpStatusCode.BadRequest;
                    _response.Messages.Add("ErrorMessage: Villa Number already Exists!");
                    return BadRequest(_response);
                }
                if (await _unitOfWork.Villa.GetAsync(x => x.Id == createDTO.VillaID) == null)
                {
                    ModelState.AddModelError("ErrorMessage", "Villa Data is Not Exists!");
                    _response.IsSuccess = false;
                    _response.StatusCode = HttpStatusCode.BadRequest;
                    _response.Messages.Add("ErrorMessage: Villa Data is Not Exists!");
                    return BadRequest(_response);
                }
                if (createDTO == null)
                {
                    _response.IsSuccess = false;
                    _response.StatusCode = HttpStatusCode.BadRequest;
                    _response.Messages.Add("Null is invalid");
                    return BadRequest(_response);
                }

                VillaNumber model = _mapper.Map<VillaNumber>(createDTO);
                model.CreatedDate = DateTime.Now;
                model.UpdatedDate = DateTime.Now;
                await _unitOfWork.VillaNumber.CreateAsync(model);
                _response.Result = _mapper.Map<VillaNumberDTO>(model);
                _response.StatusCode = HttpStatusCode.Created;
                return Ok(_response);

            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.Messages.Add(ex.Message);
                _response.StatusCode = HttpStatusCode.BadRequest;
                return BadRequest(_response);
            }
        }

        [HttpDelete("{id:int}", Name = "DeleteVillaNumber")]
        public async Task<ActionResult<ApiResponse>> DeleteVilla(int id)
        {
            try
            {
                if (id == 0)
                {
                    return BadRequest();
                }
                VillaNumber villaNumber = await _unitOfWork.VillaNumber.GetAsync(x => x.VillaNo == id);
                if (villaNumber == null)
                {
                    return NotFound();
                }
                await _unitOfWork.VillaNumber.RemoveAsync(villaNumber);
                _response.StatusCode = HttpStatusCode.NoContent;
                return Ok(_response);
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.Messages.Add(ex.Message);
                return BadRequest(_response);
            }
        }

        [HttpPut("{id:int}", Name = "UpdateVillaNumber")]
        public async Task<ActionResult<ApiResponse>> UpdateVillaNumber(int id, [FromBody] VillaNumberUpdateDTO updateDTO)
        {
            try
            {
                if (id == 0 || id != updateDTO.VillaNo || updateDTO == null)
                {
                    _response.IsSuccess = false;
                    _response.StatusCode = HttpStatusCode.BadRequest;
                    return BadRequest(_response);
                }
                if (await _unitOfWork.VillaNumber.GetAsync(filter: x => x.VillaNo == id) == null)
                {
                    _response.IsSuccess = false;
                    _response.StatusCode = HttpStatusCode.NotFound;
                    return NotFound(_response);
                }
                if (await _unitOfWork.Villa.GetAsync(x => x.Id == updateDTO.VillaID) == null)
                {
                    ModelState.AddModelError("ErrorMessage", "Villa Data is Not Exists!");
                    _response.IsSuccess = false;
                    _response.StatusCode = HttpStatusCode.BadRequest;
                    _response.Messages.Add("ErrorMessage: Villa Data is Not Exists!");
                    return BadRequest(_response);
                }

                VillaNumber model = _mapper.Map<VillaNumber>(updateDTO);
                model.UpdatedDate = DateTime.UtcNow;
                await _unitOfWork.VillaNumber.UpdateAsync(model);
                _response.StatusCode = HttpStatusCode.NoContent;
                return Ok(_response);
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.Messages.Add(ex.Message);
                _response.StatusCode = HttpStatusCode.BadRequest;
                return BadRequest(_response);
            }
        }
    }
}
