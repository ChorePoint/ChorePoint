using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using ChorePoint.Domain.Enums;

namespace ChorePoint.Domain.Entities;

public class ChoreSubmission : EntityBase
{
    public int ChoreSubmissionId { get; set; }
    public int ChoreId { get; set; }
    public int KidId { get; set; }
    
    public string? Notes { get; set; }
    public ChoreApprovalStatus ApprovalStatus { get; set; }
    public int? ApprovedByParentId { get; set; }
    public DateTime? ApprovedAt { get; set; }
    public DateTime CompletedAt { get; set; }

    public Chore Chore { get; set; }
    public Kid Kid { get; set; }
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