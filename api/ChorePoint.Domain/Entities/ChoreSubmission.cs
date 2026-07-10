using ChorePoint.Domain.Enums;

namespace ChorePoint.Domain.Entities;

public class ChoreSubmission : EntityBase
{
    public int ChoreSubmissionId { get; set; }
    public int ChoreId { get; set; }
    public int ParentId { get; set; }
    public int KidId { get; set; }

    public string? ReviewNotes { get; set; }
    public ChoreApprovalStatus ApprovalStatus { get; set; }
    public DateTime? ReviewedAt { get; set; }
    public DateTime CompletedAt { get; set; }

    public Chore Chore { get; set; }
    public Parent Parent { get; set; }
    public Kid Kid { get; set; }


    public bool CompletedThisWeek(DateTime startOfWeek)
    {
        return ApprovalStatus == ChoreApprovalStatus.Approved && CompletedAt >= startOfWeek;
    }

    public void Review(string? reviewNotes, bool approve, DateTime now)
    {
        ReviewNotes = reviewNotes;
        ApprovalStatus = approve ? ChoreApprovalStatus.Approved : ChoreApprovalStatus.Rejected;
        ReviewedAt = now;
    }
}