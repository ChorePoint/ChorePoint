using ChorePoint.Domain.Enums;

namespace ChorePoint.Application.Handlers.ChoreSubmission.GetSubmissions;

public record GetSubmissionsResponse(
    int Id,
    int ChoreId,
    int UserId,
    DateTime CompletedAt,
    ChoreApprovalStatus ApprovalStatus,
    DateTime? ApprovedAt,
    int? ApprovedByUserId,
    string? Notes,
    DateTime? CreatedAt
);