using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Employee_Management_System.Models
{
    public class Employee
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        //[Required]
        public string Name { get; set; }
        public string Email { get; set; }
        public string Phone { get; set;}

        //[ForeignKey("Department")]
        //public string Department { get; set;}
        public string Department { get; set; }
        //public Department Department { get; set; }

        //[ForeignKey("Designation")]
        //public string Designation { get; set;}
        public string Designation { get; set; }
       // public Designation Designation { get; set; }
        public string Qualification { get; set; }

    }
}
