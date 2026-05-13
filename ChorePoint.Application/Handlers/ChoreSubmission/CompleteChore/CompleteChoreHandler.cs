using ChorePoint.Application.Interfaces;
using ChorePoint.Domain.Exceptions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using ZiggyCreatures.Caching.Fusion;

namespace ChorePoint.Application.Handlers.ChoreSubmission.CompleteChore;

public class CompleteChoreHandler(IAppDbContext context, IFusionCache cache) : IRequestHandler<CompleteChoreCommand>
{
    public async Task Handle(CompleteChoreCommand request, CancellationToken cancellationToken)
    {
        var chore = await cache.GetOrSetAsync<Domain.Entities.Chore?>(
            $"chore:{request.Id}",
            async _ => await GetChoreByIdFromDb(request, cancellationToken),
            token: cancellationToken
        );

        var currentSubmission = await cache.GetOrSetAsync<Domain.Entities.ChoreSubmission?>(
            $"chore_submission:{request.Id}",
            async _ => await GetCurrentSubmissionFromDb(request, cancellationToken),
            token: cancellationToken
        );

        if (chore == null)
            throw new NotFoundException($"No chores exist with id: {request.Id}");

        var now = DateTime.UtcNow;
        chore.EnsureCanBeCompleted(currentSubmission, now);
        var newSubmission = chore.CreateSubmission(now);

        await context.ChoreSubmissions.AddAsync(newSubmission, cancellationToken);
        await context.SaveChangesAsync(cancellationToken);
    }

    private async Task<Domain.Entities.Chore?> GetChoreByIdFromDb(CompleteChoreCommand request,
        CancellationToken cancellationToken)
    {
        var chore = await context.Chores
            .FindAsync([request.Id], cancellationToken);

        return chore;
    }

    private async Task<Domain.Entities.ChoreSubmission?> GetCurrentSubmissionFromDb(CompleteChoreCommand request,
        CancellationToken cancellationToken)
    {
        var lastSubmission = await context.ChoreSubmissions
            .Where(c => c.ChoreId == request.Id)
            .OrderByDescending(c => c.CompletedAt)
            .FirstOrDefaultAsync(cancellationToken);

        return lastSubmission;
    }
}