using System.ComponentModel.DataAnnotations;

namespace Web.Models.Dto.VillaNumber
{
    public class VillaNumberCreateDTO
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
