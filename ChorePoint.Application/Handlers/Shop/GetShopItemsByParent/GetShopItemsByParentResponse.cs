using ChorePoint.Domain.Enums;

namespace ChorePoint.Application.Handlers.Shop.GetShopItemsByParent;

public record GetShopItemsByParentResponse(
    int Id,
    int ParentId,
    int KidId,
    string Name,
    int Cost,
    ShopItemStatus Status,
    int Quantity
);