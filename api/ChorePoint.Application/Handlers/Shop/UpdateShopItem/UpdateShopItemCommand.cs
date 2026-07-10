using ChorePoint.Domain.Representations;
using MediatR;

namespace ChorePoint.Application.Handlers.Shop.UpdateShopItem;

public record UpdateShopItemCommand(
    int ShopItemId,
    int? CategoryId,
    string Name,
    string Icon,
    string? Description,
    int Cost,
    int? Quantity,
    IReadOnlyList<AssignedKidToShopItem> AssignedKids
) : IRequest;
