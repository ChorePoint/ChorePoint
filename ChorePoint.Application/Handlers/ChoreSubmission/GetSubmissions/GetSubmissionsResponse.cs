using ChorePoint.Domain.Enums;
using ChoreE = ChorePoint.Domain.Entities.Chore;

namespace ChorePoint.Application.Handlers.ChoreSubmission.GetSubmissions;

public record GetSubmissionsResponse(
    int ChoreSubmissionId,
    int ChoreId,
    int KidId,
    string? Notes,
    ChoreApprovalStatus ApprovalStatus,
    int? ApprovedByParentId,
    DateTime? ApprovedAt,
    DateTime CompletedAt,
    ChoreE Chore
);