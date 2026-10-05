using ChorePoint.Application.Authorisation;
using ChorePoint.Application.Interfaces;
using ChorePoint.Domain.Enums;
using ChorePoint.Domain.Exceptions;
using ChorePoint.Domain.Services;

using MediatR;

using Microsoft.EntityFrameworkCore;

namespace ChorePoint.Application.RequestHandlers.ChoreSubmission.ReviewSubmission;

public class ReviewSubmissionHandler(IAppDbContext context, IParentContextService parentContextService) : IRequestHandler<ReviewSubmissionCommand>
{
    public async Task Handle(ReviewSubmissionCommand request, CancellationToken cancellationToken)
    {
        var choreSubmission = await context.ChoreSubmissions
            .Include(cs => cs.Kid.KidChores)
            .Include(cs => cs.Chore)
            .Where(cs =>
                cs.ChoreSubmissionId.Equals(request.ChoreSubmissionId)
                && cs.ApprovalStatus.Equals(ChoreApprovalStatus.Pending)
            )
            .SingleOrDefaultAsync(cancellationToken);

        if (choreSubmission is null)
        {
            throw new NotFoundException($"No pending chore submission exists with ID [{request.ChoreSubmissionId}]");
        }

        var parentId = parentContextService.GetParentId();
        AuthorisationHelper.EnsureParentOwnsResource(choreSubmission.ParentId, parentId);

        var kid = choreSubmission.Kid;
        var chore = choreSubmission.Chore;
        var kidChore = kid.KidChores.Single(kc => kc.KidId.Equals(choreSubmission.KidId));
        var now = DateTime.UtcNow;
        ChoreSubmissionReviewService.Review(request.ReviewNotes, request.Approve, choreSubmission, kid, chore, kidChore, now);

        await context.SaveChangesAsync(cancellationToken);
    }
}
