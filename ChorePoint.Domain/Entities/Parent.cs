namespace ChorePoint.Domain.Entities;

public class Parent : EntityBase
{
    public int ParentId { get; set; }

    public string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string Password { get; set; } = string.Empty;

    public ParentSettings ParentSettings { get; set; } = null!;
    public ICollection<Category> Categories { get; set; } = new List<Category>();

    public static Parent Create(string firstName, string lastName, string email, string password)
    {
        return new Parent
        {
            FirstName = firstName,
            LastName = lastName,
            Email = email,
            Password = password
        };
    }

    public void AddDefaultSettings(string? ianaTimeZone)
    {
        ParentSettings = new ParentSettings
        {
            IanaTimeZone = ianaTimeZone ?? ParentSettings.DefaultTimeZone,
            AutoApproveChores = false,
            ApprovePurchases = true,
            RequirePhotoEvidence = false,
            ShopOpeningDays = [DayOfWeek.Monday, DayOfWeek.Tuesday, DayOfWeek.Wednesday, DayOfWeek.Thursday, DayOfWeek.Friday, DayOfWeek.Saturday, DayOfWeek.Sunday],
            ClosedShopOnlyGatesPurchasing = false
        };
    }
}
