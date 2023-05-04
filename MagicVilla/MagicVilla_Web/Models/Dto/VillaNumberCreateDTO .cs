using MagicVilla_Web.Models.VM;
using System.ComponentModel.DataAnnotations;

namespace MagicVilla_Web.Models.Dto
{
    public class VillaNumberCreateDTO
    {
        [Required]
        public int VillaNo { get; set; }
        [Required]
        public int VillaID { get; set; }
        public string SpecialDetails { get; set; }

        public static implicit operator VillaNumberCreateDTO(VillaNumberCreateVM v)
        {
            throw new NotImplementedException();
        }
    }
}
