using MediatR;

namespace ChorePoint.Application.Handlers.ChoreSubmission.ReviewSubmission;

public record ReviewSubmissionCommand(
    int ChoreSubmissionId,
    bool Approve
) : IRequest;