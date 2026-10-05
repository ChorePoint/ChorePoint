using ChorePoint.Domain.Enums;

using ChoreE = ChorePoint.Domain.Entities.Chore;

namespace ChorePoint.Application.RequestHandlers.ChoreSubmission.GetSubmissionsByParent;

public record GetSubmissionsByParentResponse(
    int ChoreSubmissionId,
    string? ReviewNotes,
    ChoreApprovalStatus ApprovalStatus,
    DateTime? ReviewedAt,
    DateTime CompletedAt,
    ChoreE Chore
);
