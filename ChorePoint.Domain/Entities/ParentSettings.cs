namespace ChorePoint.Domain.Entities;

public class ParentSettings : EntityBase
{
    public const string DefaultTimeZone = "UTC";

    public int ParentSettingsId { get; set; }
    public int ParentId { get; set; }

    public string IanaTimeZone { get; set; } = string.Empty;
    public bool AutoApproveChores { get; set; }
    public bool ApprovePurchases { get; set; }
    public bool RequirePhotoEvidence { get; set; }
    public IReadOnlyList<DayOfWeek> ShopOpeningDays { get; set; } = new List<DayOfWeek>();
    public bool ClosedShopOnlyGatesPurchasing { get; set; }
}
