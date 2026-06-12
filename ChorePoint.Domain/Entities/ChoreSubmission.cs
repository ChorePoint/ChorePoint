using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using ChorePoint.Domain.Enums;

namespace ChorePoint.Domain.Entities;

[Table("chore_submissions")]
public class ChoreSubmission : EntityBase
{
    [Key] [Column("chore_submission_id")] public int ChoreSubmissionId { get; set; }

    [Column("chore_id")] public int ChoreId { get; set; }

    [Column("kid_id")] public int KidId { get; set; }

    [MaxLength(300)] [Column("notes")] public string? Notes { get; set; }

    [MaxLength(10)]
    [Column("approval_status")]
    public ChoreApprovalStatus ApprovalStatus { get; set; }

    [Column("completed_at")] public DateTime CompletedAt { get; set; }

    [Column("approved_at")] public DateTime? ApprovedAt { get; set; }

    [Column("approved_by_user_id")] public int? ApprovedByParentId { get; set; }


    [ForeignKey(nameof(ChoreId))] public Chore Chore { get; set; }

    [ForeignKey(nameof(KidId))] public Kid Kid { get; set; }

    [ForeignKey(nameof(ApprovedByParentId))]
    public Parent? ApprovedByParent { get; set; }


    public bool CompletedThisWeek(DateTime startOfWeek)
    {
        return ApprovalStatus == ChoreApprovalStatus.Approved && CompletedAt >= startOfWeek;
    }

    public void Review(bool approve, DateTime approvedAt, int parentId)
    {
        ApprovalStatus = approve ? ChoreApprovalStatus.Approved : ChoreApprovalStatus.Rejected;
        ApprovedAt = approvedAt;
        ApprovedByParentId = parentId;
    }
}