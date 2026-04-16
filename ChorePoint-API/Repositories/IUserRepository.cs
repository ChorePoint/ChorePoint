using ChorePoint_API.Models;

namespace ChorePoint_API.Repositories
{
    public interface IUserRepository : IRepository<User>
    {
        Task<IEnumerable<User>> GetKidsByParentId(int parentId);
    }
}
