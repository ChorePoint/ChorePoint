using ChorePoint_API.Models;
using Microsoft.EntityFrameworkCore;

namespace ChorePoint_API.Repositories
{
    public class ChoreCompletionRepository : Repository<ChoreCompletion>, IChoreCompletionRepository
    {
        public ChoreCompletionRepository(AppDbContext context) : base(context)
        {
        }

        public async Task<ChoreCompletion?> GetLastCompletionForChoreAsync(int choreId)
        {
            return await _context.ChoreCompletions
                .Where(c => c.ChoreId == choreId)
                .OrderByDescending(c => c.CompletedAt)
                .FirstOrDefaultAsync();
        }
    }
}