using Employee_Management_System.Data;
using Employee_Management_System.Models;
using Employee_Management_System.Repository.IRepository;

namespace Employee_Management_System.Repository
{
    public class EmployeeRepository : Repository<Employee>, IEmployeeRepository
    {
        private readonly ApplicationDbContext _db;

        public EmployeeRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;

        }
        public async Task UpdateAsync(Employee entity)
        {
            _db.Employees.Update(entity);
            await _db.SaveChangesAsync();
        }
    }
}
