using Main.Models;
using Main.Models.Dto;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;

namespace Main.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VillaAPIController : ApiBaseController
    {
        public VillaAPIController(IServiceProvider provider) : base(provider) { }

        [HttpGet]
        [ProducesResponseType(typeof(int), 200)]
        public async Task<ActionResult<IEnumerable<VillaDTO>>> GetVillas()
        {
            try
            {
                IEnumerable<Villa> villaList = await _unitOfWork.Villa.GetAllAsync();
                return Ok(_mapper.Map<List<VillaDTO>>(villaList));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("{id:int}", Name = "GetVilla")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(VillaDTO))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<VillaDTO>> GetVilla(int id)
        {
            if (id == 0)
            {
                return BadRequest();
            }

            try
            {
                Villa villa = await _unitOfWork.Villa.GetAsync(filter: x => x.Id == id);
                if (villa == null) { return NotFound(); }
                return Ok(_mapper.Map<VillaDTO>(villa));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        //[ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<VillaDTO>> CreateVilla([FromBody] VillaCreateDTO createDTO)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            if (await _unitOfWork.Villa.GetAsync(x => x.Name.ToLower() == createDTO.Name.ToLower()) != null)
            {
                ModelState.AddModelError("ErrorMessage", "Villa Name already Exists!");
                return BadRequest(ModelState);
            }
            if (createDTO == null)
            {
                return BadRequest(createDTO);
            }

            Villa model = _mapper.Map<Villa>(createDTO);
            try
            {
                await _unitOfWork.Villa.CreateAsync(model);

                // return 200
                return Ok(new { Success = true, Message = "" });

                // reutrn 201
                //return CreatedAtRoute("GetVilla", new { id = model.Id }, model);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("{id:int}", Name = "DeleteVilla")]
        public async Task<IActionResult> DeleteVilla(int id)
        {
            if (id == 0)
            {
                return BadRequest();
            }
            Villa villa = await _unitOfWork.Villa.GetAsync(x => x.Id == id);
            if (villa == null)
            {
                return NotFound();
            }

            try
            {
                await _unitOfWork.Villa.RemoveAsync(villa);

                // Cutome Message for Response
                return Ok(new { Success = true, Message = "OK" });

                // return 204
                //return NoContent();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }

        // Put: For update multiple properties
        [HttpPut("{id:int}", Name = "UpdateVilla")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> UpdateVilla(int id, [FromBody] VillaUpdateDTO updateDTO)
        {
            if (id == 0 || id != updateDTO.Id || updateDTO == null)
            {
                return BadRequest();
            }
            Villa villa = await _unitOfWork.Villa.GetAsync(filter: x => x.Id == id, tracked: true);
            if (villa == null)
            {
                return NotFound();
            }

            Villa model = _mapper.Map<Villa>(updateDTO);

            try
            {
                var result = await _unitOfWork.Villa.UpdateAsync(model);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // Patch: For update partical properties
        [HttpPatch("{id:int}", Name = "UpdatePartialVilla")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> UpdatePartialVilla(int id, JsonPatchDocument<VillaUpdateDTO> patchDTO)
        {
            if (patchDTO == null || id == 0)
            {
                return BadRequest();
            }
            Villa villa = await _unitOfWork.Villa.GetAsync(filter: x => x.Id == id, tracked: true);
            if (villa == null)
            {
                return NotFound();
            }

            VillaUpdateDTO villaDTO = _mapper.Map<VillaUpdateDTO>(villa);

            // referto: https://jsonpatch.com/
            patchDTO.ApplyTo(villaDTO, ModelState);
            Villa model = _mapper.Map<Villa>(villaDTO);

            try
            {
                await _unitOfWork.Villa.UpdateAsync(model);

                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }
                return Ok();
            } catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
