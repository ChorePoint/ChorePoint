using MediatR;

namespace ChorePoint.Application.Handlers.Shop.ReviewShopItemPurchase;

public record ReviewShopItemPurchaseCommand(
    int KidId,
    int ShopItemId,
    bool Approve = true
) : IRequest;