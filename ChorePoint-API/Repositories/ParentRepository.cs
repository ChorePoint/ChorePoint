using ChorePoint_API.Models;
using Microsoft.EntityFrameworkCore;

namespace ChorePoint_API.Repositories
{
    public class ParentRepository : Repository<Parent>, IParentRepository
    {
        public ParentRepository(AppDbContext context) : base(context)
        {
        }

        public async Task<Parent?> GetByEmail(string email)
        {
            return await _context.Parents
                .FirstOrDefaultAsync(p => p.Email == email);
        }

        public async Task Create(Parent parent)
        {
            await _context.Parents.AddAsync(parent);
            await _context.SaveChangesAsync();
        }
    }
}
