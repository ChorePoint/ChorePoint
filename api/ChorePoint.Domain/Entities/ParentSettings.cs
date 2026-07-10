namespace ChorePoint.Domain.Entities;

public class ParentSettings : EntityBase
{
    public int ParentSettingsId { get; set; }
    public int ParentId { get; set; }

    public bool AutoApproveChores { get; set; }
    public bool ApprovePurchases { get; set; }
    public bool RequirePhotoEvidence { get; set; }
    public IReadOnlyList<DayOfWeek> ShopOpeningDays { get; set; }

    public Parent Parent { get; set; }
}
