using System.ComponentModel.DataAnnotations;

namespace Employee_Management_System.Models.Dto
{
    public class DesignationUpadteDTO
    {
        [RegularExpression("^[a-zA-Z0-9]*$", ErrorMessage = "Only Alphabets and Numbers allowed.")]
        public int Id { get; set; }
        public string DesigCode { get; set; }
        public string DesigName { get; set; }
    }
}
