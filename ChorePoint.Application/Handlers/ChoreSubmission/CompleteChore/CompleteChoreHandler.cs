using ChorePoint.Application.Interfaces;
using ChorePoint.Domain.Exceptions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using ZiggyCreatures.Caching.Fusion;
using ChoreE = ChorePoint.Domain.Entities.Chore;
using ChoreSubmissionE = ChorePoint.Domain.Entities.ChoreSubmission;

namespace ChorePoint.Application.Handlers.ChoreSubmission.CompleteChore;

public class CompleteChoreHandler(IAppDbContext context, IFusionCache cache) : IRequestHandler<CompleteChoreCommand>
{
    public async Task Handle(CompleteChoreCommand request, CancellationToken cancellationToken)
    {
        var chore = await cache.GetOrSetAsync<ChoreE?>(
            $"chore:{request.ChoreId}",
            async _ => await GetChoreByIdFromDb(request.ChoreId, cancellationToken),
            token: cancellationToken
        );

        var currentSubmission = await cache.GetOrSetAsync<ChoreSubmissionE?>(
            $"complete_chore_chore_submission:{request.ChoreId}",
            async _ => await GetCurrentSubmissionFromDb(request.ChoreId, cancellationToken),
            token: cancellationToken
        );

        if (chore == null)
            throw new NotFoundException($"No chore exists with ID [{request.ChoreId}]");

        var now = DateTime.UtcNow;
        chore.EnsureCanBeCompleted(currentSubmission, now);
        var newSubmission = chore.CreateSubmission(now);

        await context.ChoreSubmissions.AddAsync(newSubmission, cancellationToken);
        await context.SaveChangesAsync(cancellationToken);
    }

    private async Task<ChoreE?> GetChoreByIdFromDb(int choreId, CancellationToken cancellationToken)
    {
        return await context.Chores
            .FindAsync([choreId], cancellationToken);
    }

    private async Task<ChoreSubmissionE?> GetCurrentSubmissionFromDb(int choreId,
        CancellationToken cancellationToken)
    {
        return await context.ChoreSubmissions
            .Where(cs => cs.ChoreId == choreId)
            .OrderByDescending(cs => cs.CompletedAt)
            .FirstOrDefaultAsync(cancellationToken);
    }
}