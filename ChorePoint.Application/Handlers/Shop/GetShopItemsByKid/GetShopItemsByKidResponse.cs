using ChorePoint.Domain.Enums;

namespace ChorePoint.Application.Handlers.Shop.GetShopItemsByKid;

public record GetShopItemsByKidResponse(
    int Id,
    int ParentId,
    int KidId,
    string Name,
    string? Description,
    int Cost,
    ShopItemStatus Status,
    int Quantity
);