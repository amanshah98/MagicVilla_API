using Employee_Management_System.Data;
using Employee_Management_System.Models;
using Employee_Management_System.Repository.IRepository;

namespace Employee_Management_System.Repository
{

    public class DesignationRepository : Repository<Designation>, IDesignationRepository
    {
        private readonly ApplicationDbContext _db;
        public DesignationRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }

        public async Task UpdateAsync(Designation entity)
        {
            _db.Designations.Update(entity);
            await _db.SaveChangesAsync();
        }
    }
}
