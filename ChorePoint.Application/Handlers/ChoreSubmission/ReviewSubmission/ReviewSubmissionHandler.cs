using ChorePoint.Application.Interfaces;
using ChorePoint.Domain.Enums;
using ChorePoint.Domain.Exceptions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using ZiggyCreatures.Caching.Fusion;
using ChoreSubmissionE = ChorePoint.Domain.Entities.ChoreSubmission;

namespace ChorePoint.Application.Handlers.ChoreSubmission.ReviewSubmission;

public class ReviewSubmissionHandler(IAppDbContext context, IParentContextService parentContextService, IFusionCache cache) : IRequestHandler<ReviewSubmissionCommand>
{
    public async Task Handle(ReviewSubmissionCommand request, CancellationToken cancellationToken)
    {
        var submission = await cache.GetOrSetAsync<ChoreSubmissionE?>(
            $"review_submission:{request.ChoreSubmissionId}:{request.Approve}",
            async _ => await GetPendingSubmissionFromDb(request.ChoreSubmissionId, cancellationToken),
            token: cancellationToken
            );

        if (submission == null)
            throw new NotFoundException($"No pending chore submission exists with ID [{request.ChoreSubmissionId}]");

        var parentId = parentContextService.GetParentId();

        submission.ApprovalStatus = request.Approve ? ChoreApprovalStatus.Approved : ChoreApprovalStatus.Rejected;
        submission.ApprovedAt = DateTime.UtcNow;
        submission.ApprovedByUserId = request.Approve ? parentId : null;

        await context.SaveChangesAsync(cancellationToken);
    }

    private async Task<ChoreSubmissionE?> GetPendingSubmissionFromDb(int choreSubmissionId,
        CancellationToken cancellationToken)
    {
        return await context.ChoreSubmissions
            .Where(cs => cs.Id == choreSubmissionId &&
                cs.ApprovalStatus == ChoreApprovalStatus.Pending)
            .FirstOrDefaultAsync(cancellationToken);
    }
}