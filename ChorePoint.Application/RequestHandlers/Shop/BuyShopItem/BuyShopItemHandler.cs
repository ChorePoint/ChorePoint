using ChorePoint.Application.Authorisation;
using ChorePoint.Application.Interfaces;
using ChorePoint.Application.Policies;
using ChorePoint.Domain.Enums;
using ChorePoint.Domain.Exceptions;
using ChorePoint.Domain.Services;

using MediatR;

using Microsoft.EntityFrameworkCore;

namespace ChorePoint.Application.RequestHandlers.Shop.BuyShopItem;

file sealed class BuyShopItemHandler(IAppDbContext context, IParentContextService parentContextService, IShopOperationPolicy shopOperationPolicy)
    : IRequestHandler<BuyShopItemCommand>
{
    public async Task Handle(BuyShopItemCommand request, CancellationToken cancellationToken)
    {
        await shopOperationPolicy.EnsureShopOperationIsValidIfKid(ShopOperationToGate.Purchasing, cancellationToken);

        var shopItem = await context.ShopItems
            .Include(si => si.KidShopItems)
            .Where(si => si.KidShopItems.Any(ksi => ksi.KidId.Equals(request.KidId)))
            .SingleOrDefaultAsync(si => si.ShopItemId.Equals(request.ShopItemId), cancellationToken);

        if (shopItem is null)
        {
            throw new NotFoundException($"No shop item exists with ID [{request.ShopItemId}]");
        }

        var parentId = parentContextService.GetParentId();
        AuthorisationHelper.EnsureParentOwnsResource(shopItem.ParentId, parentId);

        var kidShopItem = shopItem.KidShopItems.Single(ksi => ksi.KidId.Equals(request.KidId));
        var kid = await context.Kids.FindAsync([kidShopItem.KidId], cancellationToken);

        if (kid is null)
        {
            throw new NotFoundException($"No kid exists with ID [{kidShopItem.KidId}]");
        }

        var purchaseRequiresApproval = await context.ParentSettings
            .Where(ps => ps.ParentId.Equals(parentId))
            .Select(ps => ps.ApprovePurchases)
            .SingleOrDefaultAsync(cancellationToken);

        ShopItemPurchaseService.Purchase(purchaseRequiresApproval, kidShopItem, shopItem, kid);

        await context.SaveChangesAsync(cancellationToken);
    }
}
