using MediatR;

namespace ChorePoint.Application.RequestHandlers.Shop.DeleteShopItem;

public record DeleteShopItemCommand(int ShopItemId) : IRequest;
