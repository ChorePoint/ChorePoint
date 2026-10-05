using MediatR;

namespace ChorePoint.Application.RequestHandlers.ChoreSubmission.ReviewSubmission;

public record ReviewSubmissionCommand(int ChoreSubmissionId, string? ReviewNotes, bool Approve = true) : IRequest;
