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
            $"get_current:{request.KidId}",
            async _ => await GetCurrentSubmissionFromDb(request.KidId, cancellationToken),
            token: cancellationToken
        );

        return currentSubmission.Adapt<GetCurrentResponse>()
               ?? throw new NotFoundException($"No submission exists for kid ID [{request.KidId}]");
    }

    private async Task<Domain.Entities.ChoreSubmission?> GetCurrentSubmissionFromDb(int kidId,
        CancellationToken cancellationToken)
    {
        return await context.ChoreSubmissions
            .Where(cs => cs.KidId == kidId)
            .OrderByDescending(cs => cs.CompletedAt)
            .FirstOrDefaultAsync(cancellationToken);
    }
}