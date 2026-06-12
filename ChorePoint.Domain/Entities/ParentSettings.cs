using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ChorePoint.Domain.Entities;

[Table("parent_settings")]
public class ParentSettings : EntityBase
{
    [Key] [Column("parent_settings_id")] public int ParentSettingsId { get; set; }

    [Column("parent_id")] public int ParentId { get; set; }

    [Column("auto_approve_chores")] public bool AutoApproveChores { get; set; }

    [Column("approve_purchases")] public bool ApprovePurchases { get; set; }

    [Column("require_photo_evidence")] public bool RequirePhotoEvidence { get; set; }

    [Column("shop_opening_days")] public IReadOnlyList<DayOfWeek> ShopOpeningDays { get; set; }


    [ForeignKey(nameof(ParentId))] public Parent Parent { get; set; }
}