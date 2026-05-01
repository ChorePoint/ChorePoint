using ChorePoint.API.Models;

namespace ChorePoint.API.Repositories
{
    public interface IParentRepository
    {
        Task<Parent?> GetByEmail(string email);
        Task Create(Parent parent);
    }
}
