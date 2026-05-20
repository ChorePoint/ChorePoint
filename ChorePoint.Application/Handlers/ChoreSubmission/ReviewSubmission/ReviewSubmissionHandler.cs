using ChorePoint.Application.Interfaces;
using ChorePoint.Domain.Enums;
using ChorePoint.Domain.Exceptions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using ZiggyCreatures.Caching.Fusion;

namespace ChorePoint.Application.Handlers.ChoreSubmission.ReviewSubmission;

public class ReviewSubmissionHandler(IAppDbContext context, IParentContextService parentContextService, IFusionCache cache) : IRequestHandler<ReviewSubmissionCommand>
{
    public async Task Handle(ReviewSubmissionCommand request, CancellationToken cancellationToken)
    {
        var submission = await cache.GetOrSetAsync<Domain.Entities.ChoreSubmission?>(
            $"review_chore_submission_{request.ChoreSubmissionId}",
            async _ => await GetCurrentSubmissionFromDb(request.ChoreSubmissionId, cancellationToken)
        );

        if (submission == null)
            throw new NotFoundException("Chore submission not found");

        var parentId = parentContextService.GetParentId();

        submission.ApprovalStatus = request.IsApproved ? ChoreApprovalStatus.Approved : ChoreApprovalStatus.Rejected;
        submission.ApprovedAt = DateTime.UtcNow;
        submission.ApprovedByUserId = request.IsApproved ? parentId : null;

        await context.SaveChangesAsync(cancellationToken);
    }

    private async Task<Domain.Entities.ChoreSubmission?> GetCurrentSubmissionFromDb(int choreId,
        CancellationToken cancellationToken)
    {
        return await context.ChoreSubmissions
            .Where(cs => cs.ApprovalStatus == ChoreApprovalStatus.Pending)
            .FirstOrDefaultAsync(cancellationToken);
    }
}