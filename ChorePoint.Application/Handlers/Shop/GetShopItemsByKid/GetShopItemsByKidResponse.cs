using ChorePoint.Domain.Enums;

namespace ChorePoint.Application.Handlers.Shop.GetShopItemsByKid;

public record GetShopItemsByKidResponse(
    int ShopItemId,
    int ParentId,
    int KidShopItemId,
    string Name,
    int Cost,
    int Quantity,
    ShopItemStatus Status
);