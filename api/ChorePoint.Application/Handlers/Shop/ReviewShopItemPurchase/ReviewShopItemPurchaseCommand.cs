using MediatR;

namespace ChorePoint.Application.Handlers.Shop.ReviewShopItemPurchase;

public record ReviewShopItemPurchaseCommand(
    int ShopItemId,
    int KidId,
    bool Approve = true
) : IRequest;