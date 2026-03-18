namespace ChorePoint_API.Repositories
{
    public interface IChoreCompletionRepository : IRepository<ChoreCompletion>
    {
        Task<ChoreCompletion?> GetLastCompletionForChoreAsync(int choreId);
    }
}
