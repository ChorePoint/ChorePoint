using ChorePoint.Domain.Enums;
using ChoreE = ChorePoint.Domain.Entities.Chore;

namespace ChorePoint.Application.Handlers.ChoreSubmission.GetCurrent;

public record GetCurrentResponse(
    int ChoreSubmissionId,
    string? ReviewNotes,
    ChoreApprovalStatus ApprovalStatus,
    DateTime? ReviewedAt,
    DateTime CompletedAt,
    ChoreE Chore
);