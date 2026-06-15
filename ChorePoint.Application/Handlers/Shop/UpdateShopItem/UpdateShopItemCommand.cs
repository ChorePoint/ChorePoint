using ChorePoint.Domain.Enums;
using MediatR;

namespace ChorePoint.Application.Handlers.Shop.UpdateShopItem;

public record UpdateShopItemCommand(
    int ShopItemId,
    string Name,
    int Cost,
    int Quantity,
    int KidId,
    ShopItemStatus Status
) : IRequest;