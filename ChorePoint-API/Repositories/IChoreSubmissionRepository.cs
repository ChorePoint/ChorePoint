using ChorePoint_API.Models;

namespace ChorePoint_API.Repositories
{
    public interface IChoreSubmissionRepository : IRepository<ChoreSubmission>
    {
        Task<ChoreSubmission?> GetLastCompletionForChoreAsync(int choreId);
        Task<IEnumerable<ChoreSubmission>> GetChoreCompletionsByKidIdAsync(int kidId);
    }
}
