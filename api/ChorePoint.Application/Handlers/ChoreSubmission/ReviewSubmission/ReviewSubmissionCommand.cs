using MediatR;

namespace ChorePoint.Application.Handlers.ChoreSubmission.ReviewSubmission;

public record ReviewSubmissionCommand(
    int ChoreSubmissionId,
    string? ReviewNotes,
    bool Approve = true
) : IRequest;