using ChorePoint.Application.Interfaces;
using ChorePoint.Domain.Enums;
using ChorePoint.Domain.Exceptions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using ZiggyCreatures.Caching.Fusion;

namespace ChorePoint.Application.Handlers.ChoreSubmission.GetKidsStats;

public class GetKidsStatsHandler(IAppDbContext context, IFusionCache cache)
    : IRequestHandler<GetKidsStatsQuery, GetKidsStatsResponse>
{
    public async Task<GetKidsStatsResponse> Handle(GetKidsStatsQuery request, CancellationToken cancellationToken)
    {
        var choreSubmissions = await cache.GetOrSetAsync<IReadOnlyList<Domain.Entities.ChoreSubmission>>(
            $"chore_submissions:{request.Id}",
            async _ => await GetSubmissionsForUserFromDb(request, cancellationToken),
            token: cancellationToken
        );

        if (choreSubmissions.Count == 0)
            throw new NotFoundException($"No chore submissions found for user id: {request.Id}");

        var startOfWeek = DateTime.UtcNow.Date.AddDays(-(int)DateTime.UtcNow.DayOfWeek);
        return new GetKidsStatsResponse
        (
            choreSubmissions.Count(cs => cs.CompletedThisWeek(startOfWeek)),
            (int)(choreSubmissions.Count(cs => cs.ApprovalStatus == ChoreApprovalStatus.Approved) * 100.0 /
                  choreSubmissions.Count)
        );
    }

    private async Task<IReadOnlyList<Domain.Entities.ChoreSubmission>> GetSubmissionsForUserFromDb(
        GetKidsStatsQuery request, CancellationToken cancellationToken)
    {
        var choreSubmissions = await context.ChoreSubmissions
            .Where(c => c.UserId == request.Id)
            .ToListAsync(cancellationToken);

        return choreSubmissions;
    }
}