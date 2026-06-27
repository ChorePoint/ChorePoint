using ChorePoint.Application.Authorisation;
using ChorePoint.Application.Interfaces;
using ChorePoint.Domain.Enums;
using ChorePoint.Domain.Exceptions;
using ChorePoint.Domain.Extensions;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace ChorePoint.Application.Handlers.ChoreSubmission.GetSubmissions;

public class GetSubmissionsHandler(IAppDbContext context, IParentContextService parentContextService)
    : IRequestHandler<GetSubmissionsQuery, IReadOnlyList<GetSubmissionsResponse>>
{
    public async Task<IReadOnlyList<GetSubmissionsResponse>> Handle(GetSubmissionsQuery request,
        CancellationToken cancellationToken)
    {
        var parentId = parentContextService.GetParentId();

        var query = context.ChoreSubmissions
            .Include(cs => cs.Chore)
            .Where(cs => cs.ParentId.Equals(parentId));

        if (request.Pending) query = query.Where(cs => cs.ApprovalStatus.Equals(ChoreApprovalStatus.Pending));

        var choreSubmissions = await query.ToListAsync(cancellationToken);

        if (choreSubmissions.Empty())
        {
            var pendingText = request.Pending ? "pending" : string.Empty;
            throw new NotFoundException($"No {pendingText} submissions found with parent ID [{parentId}]");
        }

        var resourceParentIds = choreSubmissions.Select(cs => cs.ParentId).ToList();
        AuthorisationHelper.EnsureParentOwnsAllResources(resourceParentIds, parentId);

        var mapper = new GetSubmissionsMapper();
        return mapper.ChoreSubmissionsToGetSubmissionsResponseList(choreSubmissions);
    }
}