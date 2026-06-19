using ChorePoint.Application.Authorisation;
using ChorePoint.Application.Interfaces;
using ChorePoint.Domain.Entities;
using ChorePoint.Domain.Enums;
using ChorePoint.Domain.Exceptions;
using ChorePoint.Domain.Extensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using ZiggyCreatures.Caching.Fusion;
using ChoreSubmissionE = ChorePoint.Domain.Entities.ChoreSubmission;
using ChoreE = ChorePoint.Domain.Entities.Chore;

namespace ChorePoint.Application.Handlers.ChoreSubmission.GetKidsStats;

public class GetKidsStatsHandler(IAppDbContext context, IParentContextService parentContextService) : IRequestHandler<GetKidsStatsQuery, GetKidsStatsResponse>
{
    public async Task<GetKidsStatsResponse> Handle(GetKidsStatsQuery request, CancellationToken cancellationToken)
    {
        var choreSubmissions = await context.ChoreSubmissions
            .Where(cs => cs.KidId.Equals(request.KidId))
            .ToListAsync(cancellationToken);
        
        if (choreSubmissions.Empty())
            throw new NotFoundException($"No submissions found with kid ID [{request.KidId}]");

        var chores = await context.Chores
            .Include(c => c.KidChores)
            .Where(c => c.KidChores.Any(kc => kc.KidId.Equals(request.KidId)))
            .ToListAsync(cancellationToken);
        
        if (chores.Empty())
            throw new NotFoundException($"No chores exist with kid ID [{request.KidId}]");
        
        var resourceParentIds = chores.Select(chore => chore.ParentId).ToList();
        var parentId = parentContextService.GetParentId();
        AuthorisationHelper.EnsureParentOwnsAllResources(resourceParentIds, parentId);

        var startOfWeek = DateTime.UtcNow.Date.AddDays(-(int)DateTime.UtcNow.DayOfWeek);

        var completedThisWeek = choreSubmissions.Count(cs =>
            cs.CompletedThisWeek(startOfWeek) && cs.Chore.Frequency != ChoreFrequency.Bonus);
        var dueThisWeek = chores.Count(c => c.Frequency is ChoreFrequency.Weekly or ChoreFrequency.Daily);
        var approvalRate = (int)(choreSubmissions.Count(cs => cs.ApprovalStatus == ChoreApprovalStatus.Approved) *
            100.0 / choreSubmissions.Count);

        var dueDays = chores.Select(c => c.KidChores.Select(kc => kc.DueDay).FirstOrDefault()).ToList();
        var dueToday = dueDays.Count(dow => dow.Equals(DateTime.Today.DayOfWeek));

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
}