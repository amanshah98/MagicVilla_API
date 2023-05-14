using AutoMapper;
using Employee_Management_System.Models;
using Employee_Management_System.Models.Dto;

namespace Employee_Management_System
{
    public class MappingConfig :Profile 
    {
        public MappingConfig()
        {
            
            CreateMap<User, UserDTO>().ReverseMap();
            CreateMap<User, UserUpdateDTO>().ReverseMap();
            CreateMap<User, UserCreateDTO>().ReverseMap();

            CreateMap<Department, DepartmentDTO>().ReverseMap();
            CreateMap<Department, DepartmentUpdateDTO>().ReverseMap();
            CreateMap<Department, DepartmentCreateDTO>().ReverseMap();

            CreateMap<Designation, DesignationDTO>().ReverseMap();
            CreateMap<Designation, DesignationUpadteDTO>().ReverseMap();
            CreateMap<Designation, DesignationCreateDTO>().ReverseMap();

            CreateMap<Employee, EmployeeDTO>().ReverseMap();
            CreateMap<Employee, EmployeeUpdateDTO>().ReverseMap();
            CreateMap<Employee, EmployeeCreateDTO>().ReverseMap();

        }
    }
}
