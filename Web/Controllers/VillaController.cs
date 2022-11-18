using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
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
					return RedirectToAction(nameof(IndexVilla));
				}
			}
			return View(model);
		}
	}
}
