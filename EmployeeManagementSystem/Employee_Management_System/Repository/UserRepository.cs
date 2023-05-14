using Employee_Management_System.Data;
using Employee_Management_System.Models;
using Employee_Management_System.Repository.IRepository;

namespace Employee_Management_System.Repository 
{
    public class UserRepository :Repository<User>, IUserRepository
    {
        private readonly ApplicationDbContext _db;
        public UserRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }
        public async Task UpdateAsync(User entity)
        {
            _db.Users.Update(entity);
            await _db.SaveChangesAsync();
        }
    }
}
