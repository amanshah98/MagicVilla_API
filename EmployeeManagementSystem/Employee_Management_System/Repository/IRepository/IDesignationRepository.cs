using Employee_Management_System.Models;
using Employee_Management_System.Models.Dto;

namespace Employee_Management_System.Repository.IRepository
{
    public interface IDesignationRepository : IRepository<Designation>
    {
        Task UpdateAsync(Designation entity);
    }
}
