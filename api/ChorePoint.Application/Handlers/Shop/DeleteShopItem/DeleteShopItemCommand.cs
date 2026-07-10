using MediatR;

namespace ChorePoint.Application.Handlers.Shop.DeleteShopItem;

public record DeleteShopItemCommand(int ShopItemId) : IRequest;
