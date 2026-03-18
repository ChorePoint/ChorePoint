using ChorePoint_API.Models;
using ChorePoint_API.Repositories;
using ChorePoint_API.Results;

namespace ChorePoint_API.Services
{
    public class ChoreService
    {
        private readonly IRepository<Chore> _repository;
        private readonly IChoreCompletionRepository _choreCompletionRepository;
        public ChoreService(IRepository<Chore> repository, IChoreCompletionRepository choreCompletionRepository)
        {
            _repository = repository;
            _choreCompletionRepository = choreCompletionRepository;
        }


    }
}
