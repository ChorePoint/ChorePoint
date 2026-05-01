using ChorePoint.API.Models;
using Microsoft.EntityFrameworkCore;

namespace ChorePoint.API.Repositories
{
    public class UserRepository : Repository<User>, IUserRepository
    {
        public UserRepository(AppDbContext context) : base(context)
        {
        }

        public async Task<IEnumerable<User>> GetKidsByParentId(int parentId)
        {
            return await _context.Users.Where(u => u.ParentId == parentId).ToListAsync();
        }
    }
}
