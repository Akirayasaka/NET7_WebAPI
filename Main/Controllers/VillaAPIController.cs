using Main.Models;
using Main.Models.Dto;
using Main.Store;
using Microsoft.AspNetCore.Mvc;

namespace Main.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VillaAPIController : ControllerBase
    {
        [HttpGet]
        public ActionResult<IEnumerable<VillaDTO>> GetVillas()
        {
            return Ok(VillaStore.villaList);
        }

        [HttpGet("{id:int}")]
        public ActionResult<VillaDTO> GetVilla(int id)
        {
            if (id == 0) { return BadRequest(); }
            VillaDTO villa = VillaStore.villaList.FirstOrDefault(x => x.Id == id);
            if (villa == null) { return NotFound(); }
            return Ok(villa);
        }
    }
}
