using ChorePoint.Domain.Enums;

namespace ChorePoint.Application.Handlers.Shop.GetShopItemsByParent;

public record GetShopItemsByParentResponse(
    int ShopItemId,
    int ParentId,
    string Name,
    int Cost,
    int Quantity,
    ShopItemStatus Status
);