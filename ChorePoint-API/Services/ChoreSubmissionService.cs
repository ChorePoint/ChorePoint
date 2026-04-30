using ChorePoint_API.Enums;
using ChorePoint_API.Models;
using ChorePoint_API.Repositories;
using ChorePoint_API.Results;

namespace ChorePoint_API.Services
{
    public class ChoreSubmissionService
    {
        private readonly IChoreSubmissionRepository _repository;
        private readonly IRepository<Chore> _choreRepository;

        public ChoreSubmissionService(IChoreSubmissionRepository choreCompletionRepository, IRepository<Chore> choreRepository)
        {
            _repository = choreCompletionRepository;
            _choreRepository = choreRepository;
        }

        public async Task<IEnumerable<ChoreSubmission>> GetAllCompletions()
        {
            return await _repository.GetAllAsync();
        }

        public async Task<ChoreSubmission?> GetCompletionById(int id)
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

            return chore.Frequency switch
            {
                Enums.ChoreFrequency.Daily when lastCompletion?.CompletedAt.Date == DateTime.UtcNow.Date =>
                    new ServiceResult(false, "Chore already completed today", ServiceResultCode.AlreadyCompleted),
                
                Enums.ChoreFrequency.Weekly when lastCompletion?.CompletedAt.AddDays(7) > DateTime.UtcNow =>
                    new ServiceResult(false, "Chore already completed within the last week", ServiceResultCode.AlreadyCompleted),
                
                _ => new ServiceResult(true)
            };
        }

        public async Task<IServiceResult> CompleteChore(int choreId)
        {
            var canComplete = await GetCanCompleteChore(choreId);

            if (!canComplete.Success)
                return canComplete;

            var completion = new ChoreSubmission
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

        public async Task<KidStatsDto> GetChoreCompletionStatsByKidId(int kidId)
        {
            var startOfWeek = DateTime.UtcNow.Date.AddDays(-(int)DateTime.UtcNow.DayOfWeek);

            var choreSubmissions = await _repository.GetChoreCompletionsByKidIdAsync(kidId);
            var choreSubmissionsConcrete = choreSubmissions.ToList();
            
            return new KidStatsDto
            {
                CompletedThisWeek = choreSubmissionsConcrete.Count(c =>
                    c.ApprovalStatus == ChoreApprovalStatus.Approved && c.CompletedAt >= startOfWeek),

                ApprovalRate = choreSubmissionsConcrete.Count == 0 ? 0 :
                    (int)(choreSubmissionsConcrete.Count(c => c.ApprovalStatus == ChoreApprovalStatus.Approved) * 100.0 / choreSubmissionsConcrete.Count)
            };
        }
    }
}
