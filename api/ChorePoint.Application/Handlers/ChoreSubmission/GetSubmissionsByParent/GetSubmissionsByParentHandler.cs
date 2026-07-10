using ChorePoint.Application.Authorisation;
using ChorePoint.Application.Interfaces;
using ChorePoint.Domain.Enums;
using ChorePoint.Domain.Exceptions;
using ChorePoint.Domain.Extensions;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace ChorePoint.Application.Handlers.ChoreSubmission.GetSubmissionsByParent;

public class GetSubmissionsByParentHandler(IAppDbContext context, IParentContextService parentContextService)
    : IRequestHandler<GetSubmissionsByParentQuery, IReadOnlyList<GetSubmissionsByParentResponse>>
{
    public async Task<IReadOnlyList<GetSubmissionsByParentResponse>> Handle(GetSubmissionsByParentQuery request,
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

        var mapper = new GetSubmissionsByParentMapper();
        return mapper.ChoreSubmissionsToGetSubmissionsByParentResponseList(choreSubmissions);
    }
}