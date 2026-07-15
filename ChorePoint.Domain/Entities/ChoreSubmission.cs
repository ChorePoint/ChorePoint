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

        if (!approve)
            return;

        if (Kid is null)
            throw new ArgumentException(
                "Kid needs to be included in ChoreSubmission entity retrieval when a chore is approved to add points"
            );

        if (Chore is null)
            throw new ArgumentException(
                "Chore needs to be included in ChoreSubmission entity retrieval when a chore is approved to add points"
            );

        Kid.AddPoints(Chore.Points);
    }
}
