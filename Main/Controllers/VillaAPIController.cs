using AutoMapper;
using Main.Data;
using Main.Logging;
using Main.Models;
using Main.Models.Dto;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Main.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VillaAPIController : ControllerBase
    {
        private readonly ApplicationDbContext _db;
        private readonly IMapper _mapper;

        public VillaAPIController(ApplicationDbContext db, IMapper mapper)
        {
            _db = db;
            _mapper = mapper;
        }

        [HttpGet]
        [ProducesResponseType(typeof(int), 200)]
        public async Task<ActionResult<IEnumerable<VillaDTO>>> GetVillas()
        {
            IEnumerable<Villa> villaList = await _db.Villas.ToListAsync();
            return Ok(_mapper.Map<List<VillaDTO>>(villaList));
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
            Villa villa = await _db.Villas.FirstOrDefaultAsync(x => x.Id == id);
            if (villa == null) { return NotFound(); }
            return Ok(_mapper.Map<VillaDTO>(villa));
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
            if (await _db.Villas.FirstOrDefaultAsync(x => x.Name.ToLower() == createDTO.Name.ToLower()) != null)
            {
                ModelState.AddModelError("ErrorMessage", "Villa Name already Exists!");
                return BadRequest(ModelState);
            }
            if (createDTO == null)
            {
                return BadRequest(createDTO);
            }

            Villa model = _mapper.Map<Villa>(createDTO);

            //Villa model = new()
            //{
            //    Name = createDTO.Name,
            //    Details = createDTO.Details,
            //    Amenity = createDTO.Amenity,
            //    Sqft = createDTO.Sqft,
            //    Occupancy = createDTO.Occupancy,
            //    Rate = createDTO.Rate,
            //    ImageUrl = createDTO.ImageUrl,
            //    CreatedDate = DateTime.Now,
            //    UpdatedDate = DateTime.Now
            //};

            await _db.Villas.AddAsync(model);
            await _db.SaveChangesAsync();
            // return 200
            return Ok(new { Success = true, Message = "" });

            // reutrn 201
            //return CreatedAtRoute("GetVilla", new { id = model.Id }, model);
        }

        [HttpDelete("{id:int}", Name = "DeleteVilla")]
        public async Task<IActionResult> DeleteVilla(int id)
        {
            if (id == 0)
            {
                return BadRequest();
            }
            Villa villa = await _db.Villas.FirstOrDefaultAsync(x => x.Id == id);
            if (villa == null)
            {
                return NotFound();
            }

            _db.Villas.Remove(villa);
            await _db.SaveChangesAsync();

            // Cutome Message for Response
            return Ok(new { Success = true, Message = "OK" });

            // return 204
            //return NoContent();
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
            Villa villa = await _db.Villas.AsNoTracking().FirstOrDefaultAsync(x => x.Id == id);
            if (villa == null)
            {
                return NotFound();
            }

            //villa.Amenity = updateDTO.Amenity;
            //villa.Name = updateDTO.Name;
            //villa.Rate = updateDTO.Rate;
            //villa.Sqft = updateDTO.Sqft;
            //villa.Occupancy = updateDTO.Occupancy;
            //villa.ImageUrl = updateDTO.ImageUrl;
            //villa.UpdatedDate = DateTime.Now;

            Villa model = _mapper.Map<Villa>(updateDTO);
            model.UpdatedDate = DateTime.UtcNow;

            try
            {
                _db.Villas.Update(model);
                await _db.SaveChangesAsync();
                return Ok();
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
            Villa villa = await _db.Villas.AsNoTracking().FirstOrDefaultAsync(x => x.Id == id);
            if (villa == null)
            {
                return NotFound();
            }

            VillaUpdateDTO villaDTO = _mapper.Map<VillaUpdateDTO>(villa);
            //VillaUpdateDTO villaDTO = new()
            //{
            //    Id = villa.Id,
            //    Name = villa.Name,
            //    Amenity = villa.Amenity,
            //    Details = villa.Details,
            //    ImageUrl = villa.ImageUrl,
            //    Occupancy = villa.Occupancy,
            //    Rate = villa.Rate,
            //    Sqft = villa.Sqft
            //};

            // referto: https://jsonpatch.com/
            patchDTO.ApplyTo(villaDTO, ModelState);
            Villa model = _mapper.Map<Villa>(villaDTO);
            //Villa model = new()
            //{
            //    Id = villaDTO.Id,
            //    Name = villaDTO.Name,
            //    Amenity = villaDTO.Amenity,
            //    Details = villaDTO.Details,
            //    ImageUrl = villaDTO.ImageUrl,
            //    Occupancy = villaDTO.Occupancy,
            //    Rate = villaDTO.Rate,
            //    Sqft = villaDTO.Sqft
            //};

            _db.Villas.Update(model);
            await _db.SaveChangesAsync();

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            return Ok();
        }
    }
}
