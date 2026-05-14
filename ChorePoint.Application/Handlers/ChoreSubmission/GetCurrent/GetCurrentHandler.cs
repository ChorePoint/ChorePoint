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
            $"get_current:{request.UserId}",
            async _ => await GetCurrentSubmissionFromDb(request.UserId, cancellationToken),
            token: cancellationToken
        );

        return currentSubmission.Adapt<GetCurrentResponse>()
               ?? throw new NotFoundException($"No completed chores exist for user ID: {request.UserId}");
    }

    private async Task<Domain.Entities.ChoreSubmission?> GetCurrentSubmissionFromDb(int userId,
        CancellationToken cancellationToken)
    {
        var currentCompletion = await context.ChoreSubmissions
            .Where(c => c.UserId == userId)
            .OrderByDescending(c => c.CompletedAt)
            .FirstOrDefaultAsync(cancellationToken);

        return currentCompletion;
    }
}