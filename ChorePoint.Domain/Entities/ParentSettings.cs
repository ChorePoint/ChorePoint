using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ChorePoint.Domain.Entities;

[Table("parent_settings")]
public class ParentSettings
{
    [Key][Column("id")] public int Id { get; set; }

    [Required]
    [Column("parent_id")]
    public int ParentId { get; set; }

    [Required]
    [Column("auto_approve_chores")]
    public bool AutoApproveChores { get; set; } = false;

    [Required][Column("approve_purchases")] public bool ApprovePurchases { get; set; } = false;

    [Required][Column("require_photo_evidence")] public bool RequirePhotoEvidence { get; set; } = false;

    [Required]
    [Column("shop_opening_days")] public List<DayOfWeek> ShopOpeningDays { get; set; } = [];

    [Column("created_at")] public DateTime? CreatedAt { get; set; }

    [Column("updated_at")] public DateTime? UpdatedAt { get; set; } = DateTime.UtcNow;

    [ForeignKey(nameof(ParentId))] public Parent Parent { get; set; } = null!;
}