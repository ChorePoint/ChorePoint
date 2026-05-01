using ChorePoint.API.Models;

namespace ChorePoint.API.Repositories
{
    public interface IUserRepository : IRepository<User>
    {
        Task<IEnumerable<User>> GetKidsByParentId(int parentId);
    }
}
