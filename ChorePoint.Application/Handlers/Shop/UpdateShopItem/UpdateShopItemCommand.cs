using ChorePoint.Domain.Enums;
using ChorePoint.Domain.Representations;
using MediatR;

namespace ChorePoint.Application.Handlers.Shop.UpdateShopItem;

public record UpdateShopItemCommand(
    int ShopItemId,
    string Name,
    int Cost,
    int Quantity,
    IReadOnlyList<AssignedKidToShopItem> AssignedKids
) : IRequest;