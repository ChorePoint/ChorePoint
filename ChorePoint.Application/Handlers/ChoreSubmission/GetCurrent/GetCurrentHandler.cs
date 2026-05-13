using ChorePoint.Application.Interfaces;
using ChorePoint.Domain.Exceptions;
using Mapster;
using MediatR;
using Microsoft.EntityFrameworkCore;
using ZiggyCreatures.Caching.Fusion;

namespace ChorePoint.Application.Handlers.ChoreSubmission.GetCurrent;

public class GetCurrentHandler(IAppDbContext context, IFusionCache cache)
    : IRequestHandler<GetCurrentQuery, GetCurrentResponse>
{
    public async Task<GetCurrentResponse> Handle(GetCurrentQuery request, CancellationToken cancellationToken)
    {
        var currentSubmission = await cache.GetOrSetAsync<Domain.Entities.ChoreSubmission?>(
            $"chore_submission_kid:{request.Id}",
            async _ => await GetCurrentSubmissionFromDb(request, cancellationToken),
            token: cancellationToken
        );

        return currentSubmission.Adapt<GetCurrentResponse>()
               ?? throw new NotFoundException($"No completed chores exist for user id: {request.Id}");
    }

    private async Task<Domain.Entities.ChoreSubmission?> GetCurrentSubmissionFromDb(GetCurrentQuery request,
        CancellationToken cancellationToken)
    {
        var currentCompletion = await context.ChoreSubmissions
            .Where(c => c.UserId == request.Id)
            .OrderByDescending(c => c.CompletedAt)
            .FirstOrDefaultAsync(cancellationToken);

        return currentCompletion;
    }
}