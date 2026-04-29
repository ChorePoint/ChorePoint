using ChorePoint_API.Enums;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ChorePoint_API.Models
{
    [Table("chore_submissions")]
    public class ChoreSubmission
    {
        [Key]
        [Column("id")]
        public int Id { get; set; }

        [Required]
        [Column("chore_id")]
        public int ChoreId { get; set; }

        [Required]
        [Column("user_id")]
        public int UserId { get; set; }

        [Required]
        [Column("completed_at")]
        public DateTime CompletedAt { get; set; } = DateTime.UtcNow;

        [Required]
        [Column("approval_status")]
        public ChoreApprovalStatus ApprovalStatus { get; set; }
            = ChoreApprovalStatus.Pending;

        [Column("approved_at")]
        public DateTime? ApprovedAt { get; set; }

        [Column("approved_by_user_id")]
        public int? ApprovedByUserId { get; set; }

        [Column("notes")]
        public string? Notes { get; set; }

        [Column("created_at")]
        public DateTime? CreatedAt { get; set; }

        [ForeignKey(nameof(ChoreId))]
        public Chore Chore { get; set; } = null!;

        [ForeignKey(nameof(UserId))]
        public User User { get; set; } = null!;

        [ForeignKey(nameof(ApprovedByUserId))]
        public User? ApprovedBy { get; set; }
    }
}