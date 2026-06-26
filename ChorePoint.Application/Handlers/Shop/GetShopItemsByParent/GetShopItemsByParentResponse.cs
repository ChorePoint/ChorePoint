using ChorePoint.Domain.Entities;
using ChorePoint.Domain.Enums;
using ChorePoint.Domain.Representations;

namespace ChorePoint.Application.Handlers.Shop.GetShopItemsByParent;

public record GetShopItemsByParentResponse(
    int ShopItemId,
    int ParentId,
    string Name,
    string Icon,
    string? Description,
    int Cost,
    int Quantity,
    Category? Category,
    IReadOnlyList<AssignedKidToShopItem> AssignedKids
);