using System.ComponentModel.DataAnnotations;

namespace Employee_Management_System.Models.Dto
{
    public class EmployeeCreateDTO
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string Department { get; set; }
        public string Designation { get; set; }
        public string Qualification { get; set; }
    }
}
