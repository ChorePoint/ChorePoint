using ChorePoint.Domain.Enums;
using ChoreE = ChorePoint.Domain.Entities.Chore;

namespace ChorePoint.Application.Handlers.ChoreSubmission.GetLatestSubmissionByKid;

public record GetLatestSubmissionByKidResponse(
    int ChoreSubmissionId,
    string? ReviewNotes,
    ChoreApprovalStatus ApprovalStatus,
    DateTime? ReviewedAt,
    DateTime CompletedAt,
    ChoreE Chore
);
