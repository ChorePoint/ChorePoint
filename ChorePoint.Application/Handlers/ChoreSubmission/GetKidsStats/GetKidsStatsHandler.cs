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
        var choreSubmissions = await cache.GetOrSetAsync(
            $"get_kids_stats:{request.KidId}",
            async _ => await GetSubmissionsFromKidFromDb(request.KidId, cancellationToken),
            token: cancellationToken
        );

        var chores = await cache.GetOrSetAsync(
            $"get_kids_stats_chores:{request.KidId}",
            async _ => await GetChoresForKidFromDb(request.KidId, cancellationToken),
            token: cancellationToken
        );

        if (choreSubmissions.Count == 0)
            throw new NotFoundException($"No submissions found with kid ID [{request.KidId}]");

        var startOfWeek = DateTime.UtcNow.Date.AddDays(-(int)DateTime.UtcNow.DayOfWeek);

        var completedThisWeek = choreSubmissions.Count(cs =>
            cs.CompletedThisWeek(startOfWeek) && cs.Chore.Frequency != ChoreFrequency.Bonus);
        var dueThisWeek = chores.Count(c => c.Frequency is ChoreFrequency.Weekly or ChoreFrequency.Daily);
        var approvalRate = (int)(choreSubmissions.Count(cs => cs.ApprovalStatus == ChoreApprovalStatus.Approved) *
            100.0 / choreSubmissions.Count);
        var dueToday = chores.Count(c => c.DueDay == DateTime.Today.DayOfWeek);

        return new GetKidsStatsResponse
        (
            choreSubmissions.Count,
            completedThisWeek,
            approvalRate,
            dueToday,
            dueThisWeek,
            completedThisWeek / dueThisWeek * 100
        );
    }

    private async Task<IReadOnlyList<Domain.Entities.Chore>> GetChoresForKidFromDb(int kidId,
        CancellationToken cancellationToken)
    {
        return await context.Chores
            .Where(c => c.KidId == kidId)
            .ToListAsync(cancellationToken);
    }

    private async Task<IReadOnlyList<Domain.Entities.ChoreSubmission>> GetSubmissionsFromKidFromDb(int kidId,
        CancellationToken cancellationToken)
    {
        return await context.ChoreSubmissions
            .Where(cs => cs.KidId == kidId)
            .ToListAsync(cancellationToken);
    }
}