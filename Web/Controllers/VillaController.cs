using AutoMapper;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Data;
using Web.Models;
using Web.Models.Dto.Villa;
using Web.Services.IServices;

namespace Web.Controllers
{
	public class VillaController : BaseController
	{
		public VillaController(IServiceProvider provider) : base(provider)
		{
		}

		public async Task<IActionResult> IndexVilla()
		{
			List<VillaDTO> list = new();
			var response = await _villaService.GetAllAsync<ApiResponse>();
			if (response != null && response.IsSuccess)
			{
				list = JsonConvert.DeserializeObject<List<VillaDTO>>(Convert.ToString(response.Result));
			}
			return View(list);
		}

		public async Task<IActionResult> CreateVilla()
		{
			return View();
		}

		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> CreateVilla(VillaCreateDTO model)
		{
			if (ModelState.IsValid)
			{
				var response = await _villaService.CreateAsync<ApiResponse>(model);
				if (response != null && response.IsSuccess)
				{
					TempData["success"] = "Villa created successfully";
					return RedirectToAction(nameof(IndexVilla));
				}
			}
            TempData["error"] = "Error encountered.";
            return View(model);
		}

		public async Task<IActionResult> UpdateVilla(int villaId)
		{
			var response = await _villaService.GetAsync<ApiResponse>(villaId);
			if (response != null && response.IsSuccess)
			{
                TempData["success"] = "Villa updated successfully";
                VillaDTO model = JsonConvert.DeserializeObject<VillaDTO>(Convert.ToString(response.Result));
				return View(_mapper.Map<VillaUpdateDTO>(model));
			}
            TempData["error"] = "Error encountered.";
            return NotFound();
		}

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> UpdateVilla(VillaUpdateDTO model)
        {
            if (ModelState.IsValid)
            {
                TempData["success"] = "Villa updated successfully";
                var response = await _villaService.UpdateAsync<ApiResponse>(model);
                if (response != null && response.IsSuccess)
                {
                    return RedirectToAction(nameof(IndexVilla));
                }
            }
            TempData["error"] = "Error encountered.";
            return View(model);
        }

        public async Task<IActionResult> DeleteVilla(int villaId)
        {
            var response = await _villaService.GetAsync<ApiResponse>(villaId);
            if (response != null && response.IsSuccess)
            {
                VillaDTO model = JsonConvert.DeserializeObject<VillaDTO>(Convert.ToString(response.Result));
                return View(model);
            }
            TempData["error"] = "Error encountered.";
            return NotFound();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteVilla(VillaDTO model)
        {
            var response = await _villaService.DeleteAsync<ApiResponse>(model.Id);
            if (response != null && response.IsSuccess)
            {
                TempData["success"] = "Villa deleted successfully";
                return RedirectToAction(nameof(IndexVilla));
            }
            TempData["error"] = "Error encountered.";
            return View(model);
        }
    }
}
