using AutoMapper;
using Main.Repository.IRepository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Main.Controllers
{
    /// <summary>
    /// 常用DI注入管理, API Base
    /// </summary>
    [ApiController]
    public class ApiBaseController : ControllerBase
    {
        [Inject] protected readonly IMapper _mapper = null!;
        [Inject] protected readonly IUnitOfWork _unitOfWork = null!;

        public ApiBaseController(IServiceProvider provider)
        {
            provider.Inject(this);
        }
    }
}
