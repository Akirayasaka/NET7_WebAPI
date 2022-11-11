using AutoMapper;
using Main.Models;
using Main.Models.Dto.Villa;
using Main.Models.Dto.VillaNumber;

namespace Main
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
            CreateMap<Villa, VillaDTO>();
            // Line:20 等同 CreateMap<VillaDTO, Villa>()
            CreateMap<Villa, VillaDTO>().ReverseMap();
            CreateMap<Villa, VillaCreateDTO>().ReverseMap();
            CreateMap<Villa, VillaUpdateDTO>().ReverseMap();
            #endregion

            #region VillaNumber
            CreateMap<VillaNumber, VillaNumberDTO>();
            CreateMap<VillaNumber, VillaNumberDTO>().ReverseMap();
            CreateMap<VillaNumber, VillaNumberCreateDTO>().ReverseMap();
            CreateMap<VillaNumber, VillaNumberUpdateDTO>().ReverseMap();
            #endregion
        }
    }
}
