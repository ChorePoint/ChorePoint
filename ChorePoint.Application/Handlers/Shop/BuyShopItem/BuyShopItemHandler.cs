using ChorePoint.Application.Authorisation;
using ChorePoint.Application.Interfaces;
using ChorePoint.Domain.Exceptions;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace ChorePoint.Application.Handlers.Shop.BuyShopItem;

public class BuyShopItemHandler(IAppDbContext context, IParentContextService parentContextService)
    : IRequestHandler<BuyShopItemCommand>
{
    public async Task Handle(BuyShopItemCommand request, CancellationToken cancellationToken)
    {
        var shopItem = await context.ShopItems
            .Include(si => si.KidShopItems)
            .Where(si => si.KidShopItems.Any(ksi => ksi.KidId.Equals(request.KidId)))
            .SingleOrDefaultAsync(si => si.ShopItemId.Equals(request.ShopItemId), cancellationToken);

        if (shopItem is null)
            throw new NotFoundException($"No shop item exists with ID [{request.ShopItemId}]");

        var parentId = parentContextService.GetParentId();
        AuthorisationHelper.EnsureParentOwnsResource(shopItem.ParentId, parentId);

        // No AndDefault because we already know one must exist because of the .Where() above
        var kidShopItem = shopItem.KidShopItems.Single(ksi => ksi.KidId.Equals(request.KidId));

        var kid = await context.Kids
            .FindAsync([kidShopItem.KidId], cancellationToken);

        if (kid is null)
            throw new NotFoundException($"No kid exists with ID [{kidShopItem.KidId}]");

        var approvePurchases = await context.ParentSettings
            .Where(ps => ps.ParentId.Equals(parentId))
            .Select(ps => ps.ApprovePurchases)
            .SingleOrDefaultAsync(cancellationToken);

        kidShopItem.Buy(shopItem, approvePurchases);
        kid.SpendPoints(shopItem.Cost);

        await context.SaveChangesAsync(cancellationToken);
    }
}