using ChorePoint.Application.Interfaces;
using ChorePoint.Domain.Enums;
using ChorePoint.Domain.Exceptions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using ZiggyCreatures.Caching.Fusion;

namespace ChorePoint.Application.Handlers.ChoreSubmission.ReviewSubmission;

public class ReviewSubmissionHandler(
    IAppDbContext context,
    IParentContextService parentContextService,
    IFusionCache cache) : IRequestHandler<ReviewSubmissionCommand>
{
    public async Task Handle(ReviewSubmissionCommand request, CancellationToken cancellationToken)
    {
        var submission = await context.ChoreSubmissions
            .Where(cs => cs.Id == request.ChoreSubmissionId &&
                         cs.ApprovalStatus == ChoreApprovalStatus.Pending)
            .FirstOrDefaultAsync(cancellationToken);

        if (submission == null)
            throw new NotFoundException($"No pending chore submission exists with ID [{request.ChoreSubmissionId}]");

        var parentId = parentContextService.GetParentId();

        submission.ApprovalStatus = request.Approve ? ChoreApprovalStatus.Approved : ChoreApprovalStatus.Rejected;
        submission.ApprovedAt = DateTime.UtcNow;
        submission.ApprovedByUserId = request.Approve ? parentId : null;

        await context.SaveChangesAsync(cancellationToken);
    }
}