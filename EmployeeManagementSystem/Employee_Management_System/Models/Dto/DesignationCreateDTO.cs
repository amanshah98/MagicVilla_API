using System.ComponentModel.DataAnnotations;

namespace Employee_Management_System.Models.Dto
{
    public class DesignationCreateDTO
    {
        [RegularExpression("^[a-zA-Z0-9]*$", ErrorMessage = "Only Alphabets and Numbers allowed.")]
        public string DesigCode { get; set; }
        public string DesigName { get; set; }
    }
}
