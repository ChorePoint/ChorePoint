using ChorePoint.Application.Interfaces;
using ChorePoint.Domain.Enums;
using ChorePoint.Domain.Exceptions;
using MediatR;

namespace ChorePoint.Application.Handlers.Shop.ReviewShopItemPurchase;

public class ReviewShopItemPurchaseHandler(IAppDbContext context, IParentContextService parentContextService)
    : IRequestHandler<ReviewShopItemPurchaseCommand>
{
    public async Task Handle(ReviewShopItemPurchaseCommand request, CancellationToken cancellationToken)
    {
        var shopItem = await context.ShopItems.FindAsync([request.ShopItemId], cancellationToken);

        if (shopItem is null)
            throw new NotFoundException($"No shop item exists with ID [{request.ShopItemId}]");

        var parentId = parentContextService.GetParentId();

        if (shopItem.ParentId != parentId)
            throw new DomainException(
                $"Shop item with assigned parent ID [{shopItem.ParentId}] does not belong to the logged in parent with ID [{parentId}]");

        if (shopItem.Status != ShopItemStatus.Pending)
            throw new DomainException(
                $"Shop item with ID [{request.ShopItemId}] needs to have a status of {ShopItemStatus.Pending}");

        shopItem.ResetStatus();

        if (request.Approve)
        {
            var kid = await context.Kids.FindAsync([shopItem.KidId], cancellationToken);

            if (kid is null)
                throw new NotFoundException($"No kid exists with ID [{shopItem.KidId}]");

            shopItem.Buy(false);
            kid.SpendPoints(shopItem.Cost);
        }

        await context.SaveChangesAsync(cancellationToken);
    }
}