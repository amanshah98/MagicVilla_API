using Employee_Management_System.Data;
using Employee_Management_System.Models;
using Employee_Management_System.Repository.IRepository;

namespace Employee_Management_System.Repository
{
    public class DepartmentRepository : Repository<Department>, IDepartmentRepository
    {
        private readonly ApplicationDbContext _db;

        public DepartmentRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;

        }
        public async Task UpdateAsync(Department entity)
        {
            _db.Departments.Update(entity);
            await _db.SaveChangesAsync();
        }
    }
}
