using ChorePoint.Application.Interfaces;
using ChorePoint.Domain.Exceptions;
using MediatR;

namespace ChorePoint.Application.Handlers.Shop.BuyShopItem;

public class BuyShopItemHandler(IAppDbContext context, IParentContextService parentContextService)
    : IRequestHandler<BuyShopItemCommand>
{
    public async Task Handle(BuyShopItemCommand request, CancellationToken cancellationToken)
    {
        var shopItem = await context.ShopItems.FindAsync([request.ShopItemId], cancellationToken);

        if (shopItem is null)
            throw new NotFoundException($"No shop item exists with ID [{request.ShopItemId}]");

        var parentId = parentContextService.GetParentId();

        if (shopItem.ParentId != parentId)
            throw new DomainException(
                $"Shop item with assigned parent ID [{shopItem.ParentId}] does not belong to the logged in parent with ID [{parentId}]");

        var parentSettings = await context.ParentSettings.FindAsync([shopItem.ParentId], cancellationToken);

        if (parentSettings is null)
            throw new NotFoundException($"No settings exist for parent ID [{shopItem.ParentId}]");

        shopItem.Buy(parentSettings.ApprovePurchases);

        var kid = await context.Kids.FindAsync([shopItem.KidId], cancellationToken);

        if (kid is null)
            throw new NotFoundException($"No kid exists with ID [{shopItem.KidId}]");

        kid.SpendPoints(shopItem.Cost);

        await context.SaveChangesAsync(cancellationToken);
    }
}