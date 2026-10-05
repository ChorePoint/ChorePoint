using MediatR;

namespace ChorePoint.Application.RequestHandlers.Shop.BuyShopItem;

public record BuyShopItemCommand(int ShopItemId, int KidId) : IRequest;
