using ChorePoint.API.Models;

namespace ChorePoint.API.Repositories
{
    public interface IChoreSubmissionRepository : IRepository<ChoreSubmission>
    {
        Task<ChoreSubmission?> GetLastCompletionForChoreAsync(int choreId);
        Task<IEnumerable<ChoreSubmission>> GetChoreCompletionsByKidIdAsync(int kidId);
    }
}
