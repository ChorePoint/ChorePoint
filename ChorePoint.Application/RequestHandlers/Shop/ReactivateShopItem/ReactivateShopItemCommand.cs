using MediatR;

namespace ChorePoint.Application.RequestHandlers.Shop.ReactivateShopItem;

public record ReactivateShopItemCommand(int ShopItemId, int? Quantity) : IRequest;
