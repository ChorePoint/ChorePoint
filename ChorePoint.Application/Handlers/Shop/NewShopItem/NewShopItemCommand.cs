using ChorePoint.Domain.Enums;
using MediatR;

namespace ChorePoint.Application.Handlers.Shop.NewShopItem;

public record NewShopItemCommand(
    int KidId,
    string Name,
    string? Description,
    int Cost,
    ShopItemStatus Status,
    int Quantity
) : IRequest;