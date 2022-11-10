using AutoMapper;
using Main.Models;
using Main.Models.Dto;

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
            CreateMap<Villa, VillaDTO>();

            // Line:18 等同 CreateMap<VillaDTO, Villa>()
            CreateMap<Villa, VillaDTO>().ReverseMap();

            CreateMap<Villa, VillaCreateDTO>().ReverseMap();
            CreateMap<Villa, VillaUpdateDTO>().ReverseMap();
        }
    }
}
