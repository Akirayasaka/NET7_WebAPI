using System.ComponentModel.DataAnnotations;

namespace Main.Models.Dto.VillaNumber
{
    public class VillaNumberDTO
    {
        [Required]
        public int VillaNo { get; set; }

        /// <summary>
        /// Villa Table ID
        /// </summary>
        [Required]
        public int VillaID { get; set; }
        public string SpecialDetails { get; set; }
    }
}
