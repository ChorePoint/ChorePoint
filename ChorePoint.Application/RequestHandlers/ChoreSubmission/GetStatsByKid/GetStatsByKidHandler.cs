using ChorePoint.Application.Authorisation;
using ChorePoint.Application.Extensions;
using ChorePoint.Application.Interfaces;
using ChorePoint.Domain.Exceptions;
using ChorePoint.Domain.Services;

using MediatR;

using Microsoft.EntityFrameworkCore;

namespace ChorePoint.Application.RequestHandlers.ChoreSubmission.GetStatsByKid;

public class GetStatsByKidHandler(IAppDbContext context, IParentContextService parentContextService)
    : IRequestHandler<GetStatsByKidQuery, GetStatsByKidResponse>
{
    public async Task<GetStatsByKidResponse> Handle(GetStatsByKidQuery request, CancellationToken cancellationToken)
    {
        var choreSubmissions = await context.ChoreSubmissions
            .Include(cs => cs.Chore.KidChores)
            .Where(cs => cs.KidId.Equals(request.KidId))
            .ToListAsync(cancellationToken);

        if (choreSubmissions.Empty())
        {
            throw new NotFoundException($"No submissions found with kid ID [{request.KidId}]");
        }

        var resourceParentIds = choreSubmissions.Select(cs => cs.ParentId).ToList();
        var parentId = parentContextService.GetParentId();
        AuthorisationHelper.EnsureParentOwnsAllResources(resourceParentIds, parentId);

        var chores = choreSubmissions.Select(cs => cs.Chore).ToList();
        var kidChores = chores
            .Select(c =>
                c.KidChores.Single(kc => kc.KidId.Equals(request.KidId))
            )
            .ToList();

        var now = DateTime.UtcNow;
        var completedThisWeek = KidStatsCalculatorService.CalculateNumberOfChoresCompletedThisWeek(choreSubmissions, now);
        var dueThisWeek = KidStatsCalculatorService.CalculateNumberOfChoresDueThisWeek(chores);
        return new GetStatsByKidResponse(
            choreSubmissions.Count,
            completedThisWeek,
            KidStatsCalculatorService.CalculateSubmissionApprovalRate(choreSubmissions),
            KidStatsCalculatorService.CalculateNumberOfChoresDueToday(kidChores, now),
            dueThisWeek,
            completedThisWeek / dueThisWeek * 100
        );
    }
}
