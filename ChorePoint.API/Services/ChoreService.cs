using ChorePoint.API.Repositories;

namespace ChorePoint.API.Services
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
