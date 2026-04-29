using ChorePoint_API.Models;
using ChorePoint_API.Repositories;
using ChorePoint_API.Results;

namespace ChorePoint_API.Services
{
    public class ChoreService
    {
        private readonly IChoreRepository _repository;
        public ChoreService(IChoreRepository repository)
        {
            _repository = repository;
        }
    }
}
