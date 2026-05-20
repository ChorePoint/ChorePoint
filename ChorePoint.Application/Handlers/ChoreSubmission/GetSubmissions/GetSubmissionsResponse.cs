using ChorePoint.Domain.Enums;

namespace ChorePoint.Application.Handlers.ChoreSubmission.GetSubmissions;

public record GetSubmissionsResponse(
    int Id,
    int ChoreId,
    int KidId,
    DateTime CompletedAt,
    ChoreApprovalStatus ApprovalStatus,
    DateTime? ApprovedAt,
    int? ApprovedByUserId,
    string? Notes,
    DateTime? CreatedAt,
    Domain.Entities.Chore Chore
);