using ChorePoint.Domain.Enums;

namespace ChorePoint.Domain.Representations;

public record AssignedKidToShopItem (
    int KidId,
    ShopItemStatus? Status
    );