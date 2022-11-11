using System.ComponentModel.DataAnnotations;

namespace Main.Models.Dto.VillaNumber
{
    public class VillaNumberUpdateDTO
    {
        [Required]
        public int VillaNo { get; set; }
        public string SpecialDetails { get; set; }
    }
}
