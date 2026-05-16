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
            $"get_kids_stats:{request.UserId}",
            async _ => await GetSubmissionsForUserFromDb(request.UserId, cancellationToken),
            token: cancellationToken
        );

        var chores = await cache.GetOrSetAsync(
            $"get_kids_stats_chores:{request.UserId}",
            async _ => await GetChoresForUserFromDb(request.UserId, cancellationToken),
            token: cancellationToken
        );

        if (choreSubmissions.Count == 0)
            throw new NotFoundException($"No chore submissions found for user ID: {request.UserId}");

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

    private async Task<IReadOnlyList<Domain.Entities.Chore>> GetChoresForUserFromDb(int userId,
        CancellationToken cancellationToken)
    {
        var chores = await context.Chores
            .Where(c => c.UserId == userId)
            .ToListAsync(cancellationToken);

        return chores;
    }

    private async Task<IReadOnlyList<Domain.Entities.ChoreSubmission>> GetSubmissionsForUserFromDb(int userId,
        CancellationToken cancellationToken)
    {
        var choreSubmissions = await context.ChoreSubmissions
            .Where(c => c.UserId == userId)
            .ToListAsync(cancellationToken);

        return choreSubmissions;
    }
}