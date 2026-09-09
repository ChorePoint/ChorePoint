using ChorePoint.Domain.Entities;

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
    bool PendingApproval,
    bool IsVisible
);
