using ChorePoint_API.Models;
using ChorePoint_API.Repositories;

namespace ChorePoint_API.Services
{
    public class UserService
    {
        private readonly IRepository<User> _repository;

        public UserService(IRepository<User> userRepository)
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
    }
}
