using MediatR;

namespace ChorePoint.Application.Handlers.Shop.ReactivateShopItem;

public record ReactivateShopItemCommand(
    int ShopItemId,
    int? Quantity
) : IRequest;