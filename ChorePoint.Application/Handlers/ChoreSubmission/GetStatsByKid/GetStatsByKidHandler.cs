using ChorePoint.Application.Authorisation;
using ChorePoint.Application.Interfaces;
using ChorePoint.Domain.Enums;
using ChorePoint.Domain.Exceptions;
using ChorePoint.Domain.Extensions;

using MediatR;

using Microsoft.EntityFrameworkCore;

namespace ChorePoint.Application.Handlers.ChoreSubmission.GetStatsByKid;

public class GetStatsByKidHandler(IAppDbContext context, IParentContextService parentContextService, IKidContextService kidContextService)
    : IRequestHandler<GetStatsByKidQuery, GetStatsByKidResponse>
{
    public async Task<GetStatsByKidResponse> Handle(GetStatsByKidQuery request, CancellationToken cancellationToken)
    {
        var kidId = request.KidId == null ? kidContextService.GetKidId() : (int)request!.KidId!;

        var choreSubmissions = await context.ChoreSubmissions
            .Include(cs => cs.Chore)
            .ThenInclude(c => c.KidChores)
            .Where(cs => cs.KidId.Equals(kidId))
            .ToListAsync(cancellationToken);

        if (choreSubmissions.Empty())
        {
            throw new NotFoundException($"No submissions found with kid ID [{kidId}]");
        }

        var resourceParentIds = choreSubmissions.Select(cs => cs.ParentId).ToList();
        var parentId = parentContextService.GetParentId();
        AuthorisationHelper.EnsureParentOwnsAllResources(resourceParentIds, parentId);

        var startOfWeek = DateTime.UtcNow.Date.AddDays(-(int)DateTime.UtcNow.DayOfWeek);
        var chores = choreSubmissions.Select(cs => cs.Chore).ToList();

        var completedThisWeek = choreSubmissions.Count(cs =>
            cs.CompletedThisWeek(startOfWeek) && cs.Chore.Frequency is not ChoreFrequency.Bonus
        );
        var dueThisWeek = chores.Count(c => c.Frequency is ChoreFrequency.Weekly or ChoreFrequency.Daily);
        var approvalRate = (int)(
            choreSubmissions.Count(cs => cs.ApprovalStatus is ChoreApprovalStatus.Approved)
            * 100.0
            / choreSubmissions.Count
        );

        var dueToday = chores
            .Select(c =>
                c.KidChores.Where(cs => cs.KidId.Equals(request.KidId)).Select(kc => kc.DueDay).SingleOrDefault()
            )
            .Count(dow => dow.Equals(DateTime.Today.DayOfWeek));

        return new GetStatsByKidResponse(
            choreSubmissions.Count,
            completedThisWeek,
            approvalRate,
            dueToday,
            dueThisWeek,
            completedThisWeek / dueThisWeek * 100
        );
    }
}
