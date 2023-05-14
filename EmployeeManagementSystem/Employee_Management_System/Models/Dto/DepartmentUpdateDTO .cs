using System.ComponentModel.DataAnnotations;

namespace Employee_Management_System.Models.Dto
{
    public class DepartmentUpdateDTO
    {
        [RegularExpression("^[a-zA-Z0-9]*$", ErrorMessage = "Only Alphabets and Numbers allowed.")]
        public int Id { get; set; }
        public string DepartCode { get; set; }
        public string DepartName { get; set; }
    }
}
