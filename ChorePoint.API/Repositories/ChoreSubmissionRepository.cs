using ChorePoint.API.Models;
using Microsoft.EntityFrameworkCore;

namespace ChorePoint.API.Repositories
{
    public class ChoreSubmissionRepository : Repository<ChoreSubmission>, IChoreSubmissionRepository
    {
        public ChoreSubmissionRepository(AppDbContext context) : base(context)
        {
        }

        public async Task<ChoreSubmission?> GetLastCompletionForChoreAsync(int choreId)
        {
            return await _context.ChoreCompletions
                .Where(c => c.ChoreId == choreId)
                .OrderByDescending(c => c.CompletedAt)
                .FirstOrDefaultAsync();
        }

        public async Task<IEnumerable<ChoreSubmission>> GetChoreCompletionsByKidIdAsync(int kidId)
        {
            return await _context.ChoreCompletions.Where(c => c.UserId == kidId).ToListAsync();
        }
    }
}