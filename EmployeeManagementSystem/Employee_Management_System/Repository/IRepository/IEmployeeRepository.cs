using Employee_Management_System.Models;
using Employee_Management_System.Models.Dto;

namespace Employee_Management_System.Repository.IRepository
{
    public interface IEmployeeRepository : IRepository<Employee>
    {
        Task UpdateAsync(Employee entity);
    }
}
