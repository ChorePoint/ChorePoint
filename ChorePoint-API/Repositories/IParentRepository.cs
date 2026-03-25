using ChorePoint_API.Models;

namespace ChorePoint_API.Repositories
{
    public interface IParentRepository
    {
        Task<Parent?> GetByEmail(string email);
        Task Create(Parent parent);
    }
}
