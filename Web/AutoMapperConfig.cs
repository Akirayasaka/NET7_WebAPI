using AutoMapper;
using Web.Models;
using Web.Models.Dto.Villa;
using Web.Models.Dto.VillaNumber;

namespace Web
{
    public class AutoMapperConfig : Profile
    {
        /// <summary>
        /// AutoMapper設定檔
        /// </summary>
        public AutoMapperConfig()
        {
            // Sample: CreateMap<InputClass, OutputClass>();

            #region Villa
            CreateMap<VillaDTO, VillaCreateDTO>().ReverseMap();
            CreateMap<VillaDTO, VillaUpdateDTO>().ReverseMap();
            #endregion

            #region VillaNumber
            CreateMap<VillaNumberDTO, VillaNumberCreateDTO>().ReverseMap();
            CreateMap<VillaNumberDTO, VillaNumberUpdateDTO>().ReverseMap();
            #endregion
        }
    }
}
