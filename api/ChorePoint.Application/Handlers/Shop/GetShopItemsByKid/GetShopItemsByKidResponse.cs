using ChorePoint.Domain.Entities;
using ChorePoint.Domain.Enums;

namespace ChorePoint.Application.Handlers.Shop.GetShopItemsByKid;

public record GetShopItemsByKidResponse(
    int ShopItemId,
    int ParentId,
    string Name,
    string Icon,
    string? Description,
    int Cost,
    int? Quantity,
    Category? Category,
    ShopItemStatus Status
);
