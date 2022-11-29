using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Newtonsoft.Json;
using Web.Models;
using Web.Models.Dto.Villa;
using Web.Models.Dto.VillaNumber;
using Web.Models.Dto.VM;

namespace Web.Controllers
{
    public class VillaNumberController : BaseController
    {
        public VillaNumberController(IServiceProvider provider) : base(provider) { }

        public async Task<IActionResult> IndexVillaNumber()
        {
            List<VillaNumberDTO> list = new List<VillaNumberDTO>();
            var response = await _villaNumberService.GetAllAsync<ApiResponse>();
            if (response != null && response.IsSuccess)
            {
                list = JsonConvert.DeserializeObject<List<VillaNumberDTO>>(Convert.ToString(response.Result));
            }
            return View(list);
        }

        public async Task<IActionResult> CreateVillaNumber()
        {
            VillaNumberCreateVM vm = new();
            var response = await _villaService.GetAllAsync<ApiResponse>();
            if (response != null && response.IsSuccess)
            {
                vm.VillaList = JsonConvert.DeserializeObject<List<VillaDTO>>(Convert.ToString(response.Result))
                    .Select(x => new SelectListItem { Text = x.Name, Value = x.Id.ToString() });
            }
            return View(vm);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateVillaNumber(VillaNumberCreateDTO model)
        {
            if (ModelState.IsValid)
            {
                var response = await _villaNumberService.CreateAsync<ApiResponse>(model);
                if (response != null && response.IsSuccess)
                {
                    return RedirectToAction(nameof(IndexVillaNumber));
                }
            }
            return View(model);
        }

    }
}
