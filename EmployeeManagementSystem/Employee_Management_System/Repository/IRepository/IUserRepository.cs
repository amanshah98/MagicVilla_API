using Employee_Management_System.Models;
using Employee_Management_System.Models.Dto;

namespace Employee_Management_System.Repository.IRepository
{
    public interface IUserRepository : IRepository<User>
    {
        Task UpdateAsync(User entity);
    }
}
