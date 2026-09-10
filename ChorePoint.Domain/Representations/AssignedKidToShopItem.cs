namespace ChorePoint.Domain.Representations;

public record AssignedKidToShopItem(int KidId, bool PendingApproval, bool IsVisible);
