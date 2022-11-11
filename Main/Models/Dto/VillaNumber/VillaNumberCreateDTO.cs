using System.ComponentModel.DataAnnotations;

namespace Main.Models.Dto.VillaNumber
{
    public class VillaNumberCreateDTO
    {
        [Required]
        public int VillaNo { get; set; }
        public string SpecialDetails { get; set; }
    }
}
