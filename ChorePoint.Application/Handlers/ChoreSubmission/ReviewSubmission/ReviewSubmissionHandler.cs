using ChorePoint.Application.Interfaces;
using ChorePoint.Domain.Enums;
using ChorePoint.Domain.Exceptions;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace ChorePoint.Application.Handlers.ChoreSubmission.ReviewSubmission;

public class ReviewSubmissionHandler(
    IAppDbContext context,
    IParentContextService parentContextService) : IRequestHandler<ReviewSubmissionCommand>
{
    public async Task Handle(ReviewSubmissionCommand request, CancellationToken cancellationToken)
    {
        var submission = await context.ChoreSubmissions
            .Where(cs => cs.Id == request.ChoreSubmissionId &&
                         cs.ApprovalStatus == ChoreApprovalStatus.Pending)
            .FirstOrDefaultAsync(cancellationToken);

        if (submission is null)
            throw new NotFoundException($"No pending chore submission exists with ID [{request.ChoreSubmissionId}]");

        submission.Review(request.Approve, DateTime.Now, parentContextService.GetParentId());

        await context.SaveChangesAsync(cancellationToken);
    }
}