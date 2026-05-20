using ChorePoint.Domain.Enums;
using ChoreE = ChorePoint.Domain.Entities.Chore;

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
    ChoreE Chore
);