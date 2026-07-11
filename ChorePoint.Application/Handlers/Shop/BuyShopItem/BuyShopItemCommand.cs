using MediatR;

namespace ChorePoint.Application.Handlers.Shop.BuyShopItem;

public record BuyShopItemCommand(int ShopItemId, int KidId) : IRequest;
