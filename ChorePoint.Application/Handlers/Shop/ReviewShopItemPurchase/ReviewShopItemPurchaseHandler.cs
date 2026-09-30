using ChorePoint.Application.Authorisation;
using ChorePoint.Application.Interfaces;
using ChorePoint.Domain.Exceptions;
using ChorePoint.Domain.Services;

using MediatR;

using Microsoft.EntityFrameworkCore;

namespace ChorePoint.Application.Handlers.Shop.ReviewShopItemPurchase;

public class ReviewShopItemPurchaseHandler(IAppDbContext context, IParentContextService parentContextService) : IRequestHandler<ReviewShopItemPurchaseCommand>
{
    public async Task Handle(ReviewShopItemPurchaseCommand request, CancellationToken cancellationToken)
    {
        var shopItem = await context.ShopItems
            .Include(si => si.KidShopItems)
            .SingleOrDefaultAsync(si => si.ShopItemId.Equals(request.ShopItemId), cancellationToken);

        if (shopItem is null)
        {
            throw new NotFoundException($"No shop item exists with ID [{request.ShopItemId}]");
        }

        var parentId = parentContextService.GetParentId();
        AuthorisationHelper.EnsureParentOwnsResource(shopItem.ParentId, parentId);

        var kidShopItem = shopItem.KidShopItems.SingleOrDefault(ksi => ksi.KidId.Equals(request.KidId));

        if (kidShopItem is null)
        {
            throw new DomainException(
                $"Kid with ID [{request.KidId}] is not assigned to shop item with ID [{request.ShopItemId}]"
            );
        }

        var kid = await context.Kids.FindAsync([kidShopItem.KidId], cancellationToken);

        if (kid is null)
        {
            throw new NotFoundException($"No kid exists with ID [{kidShopItem.KidId}]");
        }

        ShopItemPurchaseService.ReviewPurchase(request.Approve, kidShopItem, shopItem, kid);

        await context.SaveChangesAsync(cancellationToken);
    }
}
