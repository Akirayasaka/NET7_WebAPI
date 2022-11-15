using AutoMapper;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Mvc;
using Web.Services.IServices;
using InjectAttribute = Microsoft.Extensions.DependencyInjection.InjectAttribute;

namespace Web.Controllers
{
    public class BaseController : Controller
    {
        [Inject] protected readonly IVillaService _villaService;
        [Inject] protected readonly IMapper _mapper;

        public BaseController(IServiceProvider provider)
        {
            provider.Inject(this);
        }
    }
}
