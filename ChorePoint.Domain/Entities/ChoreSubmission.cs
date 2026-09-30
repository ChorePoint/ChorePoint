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

    public Chore Chore { get; set; } = null!;
    public Parent Parent { get; set; } = null!;
    public Kid Kid { get; set; } = null!;

    public bool ApprovedToday(DateTime now)
    {
        var today = now.Date;
        return ApprovalStatus == ChoreApprovalStatus.Approved && CompletedAt.Date.Equals(today);
    }

    public bool ApprovedThisWeek(DateTime now)
    {
        var startOfWeek = now.Date.AddDays(-(int)now.DayOfWeek);
        return ApprovalStatus == ChoreApprovalStatus.Approved && CompletedAt >= startOfWeek;
    }

    public void Review(string? reviewNotes, bool approve, DateTime now)
    {
        ReviewNotes = reviewNotes;
        ApprovalStatus = approve ? ChoreApprovalStatus.Approved : ChoreApprovalStatus.Rejected;
        ReviewedAt = now;
    }
}
