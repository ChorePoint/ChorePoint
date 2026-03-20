using ChorePoint_API.Enums;
using ChorePoint_API.Models;
using ChorePoint_API.Repositories;
using ChorePoint_API.Results;

namespace ChorePoint_API.Services
{
    public class ChoreCompletionService
    {
        private readonly IChoreCompletionRepository _repository;
        private readonly IRepository<Chore> _choreRepository;

        public ChoreCompletionService(IChoreCompletionRepository choreCompletionRepository, IRepository<Chore> choreRepository)
        {
            _repository = choreCompletionRepository;
            _choreRepository = choreRepository;
        }

        public async Task<IEnumerable<ChoreCompletion>> GetAllCompletions()
        {
            return await _repository.GetAllAsync();
        }

        public async Task<ChoreCompletion?> GetCompletionById(int id)
        {
            return await _repository.GetByIdAsync(id);
        }

        public async Task<IServiceResult> GetCanCompleteChore(int choreId)
        {
            var chore = await _choreRepository.GetByIdAsync(choreId);
            var lastCompletion = await _repository.GetLastCompletionForChoreAsync(choreId);

            if (chore == null)
            {
                return new ServiceResult(false, "Chore not found", ServiceResultCode.NotFound);
            }

            if (chore.Frequency == Enums.ChoreFrequency.Daily)
            {
                if (lastCompletion != null && lastCompletion.CompletedAt.Date == DateTime.UtcNow.Date)
                {
                    return new ServiceResult(false, "Chore already completed today", ServiceResultCode.AlreadyCompleted);
                }

            }

            if (chore.Frequency == Enums.ChoreFrequency.Weekly)
            {
                if (lastCompletion != null && lastCompletion.CompletedAt.AddDays(7) > DateTime.UtcNow)
                {
                    return new ServiceResult(false, "Chore already completed within the last week", ServiceResultCode.AlreadyCompleted);
                }

            }

            return new ServiceResult(true);
        }

        public async Task<IServiceResult> CompleteChore(int choreId)
        {
            var canComplete = await GetCanCompleteChore(choreId);

            if (!canComplete.Success)
                return canComplete;

            var completion = new ChoreCompletion
            {
                ChoreId = choreId,
                UserId = 1,
                CompletedAt = DateTime.UtcNow,
                ApprovalStatus = ChoreApprovalStatus.Approved,
                ApprovedAt = DateTime.Now
            };

            await _repository.AddAsync(completion);
            await _repository.SaveAsync();

            return new ServiceResult(true);
        }
    }
}
