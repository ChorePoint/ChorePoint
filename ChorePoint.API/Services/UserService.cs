using ChorePoint.API.Models;
using ChorePoint.API.Repositories;

namespace ChorePoint.API.Services
{
    public class UserService
    {
        private readonly IUserRepository _repository;

        public UserService(IUserRepository userRepository)
        {
            _repository = userRepository;
        }

        public async Task<IEnumerable<User>> GetAllUsers()
        {
            return await _repository.GetAllAsync();
        }

        public async Task<User?> GetUserById(int id)
        {
            return await _repository.GetByIdAsync(id);
        }

        public async Task<IEnumerable<User>> GetKidsByParentId(int parentId)
        {
            return await _repository.GetKidsByParentId(parentId);
        }
    }
}
